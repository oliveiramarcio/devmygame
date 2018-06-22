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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public IEnumerable<Usuario> Listar(int? codigo, string email, string senha)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                StringBuilder query = new StringBuilder(@" Select U.Codigo,
                                              U.Nome,
                                              U.Email,
                                              U.Senha,
                                              U.Telefone,
                                              U.DataCadastro
                                         From Usuario U
                                        Where 0 = 0");

                DynamicParameters parametros = new DynamicParameters();

                if ((codigo != null) && (codigo > 0))
                {
                    parametros.Add("Codigo", codigo);
                    query.Append(" And U.Codigo = @Codigo ");
                }

                if (!string.IsNullOrEmpty(email))
                {
                    parametros.Add("Email", email);
                    query.Append(" And UPPER(RTRIM(LTRIM(U.Email))) = UPPER(RTRIM(LTRIM(@Email))) ");
                }

                if (!string.IsNullOrEmpty(senha))
                {
                    string senhaCriptografada = Criptografia.GerarHashMD5(senha);
                    parametros.Add("Senha", senhaCriptografada);
                    query.Append(" And UPPER(U.Senha) = UPPER(@Senha) ");
                }

                query.Append(" Order By U.DataCadastro, U.Nome ");

                var listaUsuarios = conexao.Query<Usuario>(query.ToString(), parametros).ToList();

                return listaUsuarios;
            }
        }

        public Usuario Recuperar(int codigo)
        {
            return this.Listar(codigo, null, null).FirstOrDefault();
        }

        public Usuario Inserir(string nome, string email, string senha, string telefone)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    string sql = @" Insert into Usuario (Nome, Email, Senha, Telefone, DataCadastro)
                                            Values (@Nome, @Email, @Senha, @Telefone, @DataCadastro) ";

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("Nome", nome);
                    parametros.Add("Email", email);

                    string senhaCriptografada = Criptografia.GerarHashMD5(senha);
                    parametros.Add("Senha", senhaCriptografada);

                    parametros.Add("Telefone", telefone);
                    parametros.Add("DataCadastro", DateTime.Now);

                    conexao.Execute(sql, parametros, transacao);

                    transacao.Commit();

                    return this.Listar(null, email, null).FirstOrDefault();
                }
                catch (Exception e)
                {
                    transacao.Rollback();

                    throw e;
                }
            }
        }

        public Usuario Atualizar(int codigo, string nome, string email, string senha, string telefone)
        {
            using (var conexao = BancoDeDados.Conexao())
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    string sql = @" Update Usuario
                                       Set Nome = @Nome,
                                           Email = @Email,
                                           Senha = @Senha,
                                           Telefone = @Telefone
                                     Where Codigo = @Codigo ";

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("Nome", nome);
                    parametros.Add("Email", email);

                    string senhaCriptografada = Criptografia.GerarHashMD5(senha);
                    parametros.Add("Senha", senhaCriptografada);

                    parametros.Add("Telefone", telefone);
                    parametros.Add("Codigo", codigo);

                    conexao.Execute(sql, parametros, transacao);

                    transacao.Commit();

                    return this.Listar(null, email, null).FirstOrDefault();
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
                    string sql = @" Delete From Usuario Where Codigo = @Codigo ";

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("Codigo", codigo);

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

        public bool AutenticarUsuario(string email, string senha)
        {
            return (this.Listar(null, email, senha).FirstOrDefault() != null);
        }
    }
}