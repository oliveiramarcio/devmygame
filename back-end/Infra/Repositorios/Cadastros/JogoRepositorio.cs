using Biblioteca.Classes;
using Dapper;
using Dominio.Entidades.Cadastros;
using Infra.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Infra.Repositorios.Cadastros
{
    public class JogoRepositorio : IJogoRepositorio
    {
        public IEnumerable<Jogo> Listar(int? codigo, string nome, int? codigoUsuarioDono)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                StringBuilder query = new StringBuilder(@" Select J.Codigo,
                                                                  J.Nome,
                                                                  J.DataCadastro,
                                                                  J.CodigoUsuarioDono,
                                                                  (Select U.Nome
	                                                                 From EmprestimoJogo E
		                                                             Join Usuario U On (U.Codigo = E.CodigoUsuario)
		                                                            Where E.CodigoJogo = J.Codigo
		                                                              And E.DataDevolucao Is Null
		                                                              And E.DataEmprestimo = (Select Max(EJ.DataEmprestimo)
	                                                                                            From EmprestimoJogo EJ
		                                                                                       Where EJ.CodigoJogo = J.Codigo
		                                                                                         And EJ.DataDevolucao Is Null)) as NomeUsuarioEmpresitmo,
                                                                  (Select Max(E.DataEmprestimo)
	                                                                 From EmprestimoJogo E
		                                                            Where E.CodigoJogo = J.Codigo
		                                                              And E.DataDevolucao Is Null) as DataEmprestimo
                                                             From Jogo J
                                                            Where 0 = 0 ");

                DynamicParameters parametros = new DynamicParameters();

                if ((codigo != null) && (codigo > 0))
                {
                    parametros.Add("Codigo", codigo);
                    query.Append(" And J.Codigo = @Codigo ");
                }

                if (!string.IsNullOrEmpty(nome))
                {
                    parametros.Add("Nome", nome);
                    query.Append(" And J.Nome = @Nome ");
                }

                if ((codigoUsuarioDono != null) && (codigoUsuarioDono > 0))
                {
                    parametros.Add("CodigoUsuarioDono", codigoUsuarioDono);
                    query.Append(" And J.CodigoUsuarioDono = @CodigoUsuarioDono ");
                }

                query.Append(" Order By J.Nome ");

                var listaJogos = conexao.Query<Jogo>(query.ToString(), parametros).ToList();

                return listaJogos;
            }
        }

        public Jogo Recuperar(int codigo)
        {
            return this.Listar(codigo, null, null).FirstOrDefault();
        }

        public Jogo Inserir(string nome, int codigoUsuarioDono)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    string sql = @" Insert into Jogo (Nome, DataCadastro, CodigoUsuarioDono)
                                    Values (@Nome, @DataCadastro, @CodigoUsuarioDono) ";

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("Nome", nome);
                    parametros.Add("DataCadastro", DateTime.Now);
                    parametros.Add("CodigoUsuarioDono", codigoUsuarioDono);

                    conexao.Execute(sql, parametros, transacao);

                    transacao.Commit();

                    return this.Listar(null, nome, codigoUsuarioDono).FirstOrDefault();
                }
                catch (Exception e)
                {
                    transacao.Rollback();

                    throw e;
                }
            }
        }

        public Jogo Atualizar(int codigo, string nome)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    string sql = @" Update Jogo
                                       Set Nome = @Nome
                                     Where Codigo = @Codigo ";

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("Nome", nome);
                    parametros.Add("Codigo", codigo);

                    conexao.Execute(sql, parametros, transacao);

                    transacao.Commit();

                    return this.Listar(codigo, null, null).FirstOrDefault();
                }
                catch (Exception e)
                {
                    transacao.Rollback();

                    throw e;
                }
            }
        }

        public bool Excluir(int codigo)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    StringBuilder sql = new StringBuilder(@" Delete From EmprestimoJogo Where CodigoJogo = @Codigo ");

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("Codigo", codigo);

                    conexao.Execute(sql.ToString(), parametros, transacao);

                    sql.Clear();
                    sql.Append(@" Delete From Jogo Where Codigo = @Codigo ");

                    conexao.Execute(sql.ToString(), parametros, transacao);

                    transacao.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transacao.Rollback();

                    throw e;
                }
            }
        }

        public bool Devolver(int codigoJogo)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    string sql = @" Update EmprestimoJogo
                                           Set DataDevolucao = @DataDevolucao
                                         Where CodigoJogo = @CodigoJogo ";

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("DataDevolucao", DateTime.Now);
                    parametros.Add("CodigoJogo", codigoJogo);

                    conexao.Execute(sql, parametros, transacao);

                    transacao.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transacao.Rollback();

                    throw e;
                }
            }
        }

        public bool Emprestar(int codigoUsuarioEmprestimo, int codigoJogo)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    string sql = @" Insert Into EmprestimoJogo (CodigoUsuario, CodigoJogo, DataEmprestimo)
                                       Values (@CodigoUsuario, @CodigoJogo, @DataEmprestimo) ";

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("CodigoUsuario", codigoUsuarioEmprestimo);
                    parametros.Add("CodigoJogo", codigoJogo);
                    parametros.Add("DataEmprestimo", DateTime.Now);

                    conexao.Execute(sql, parametros, transacao);

                    transacao.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transacao.Rollback();

                    throw e;
                }
            }
        }
    }
}