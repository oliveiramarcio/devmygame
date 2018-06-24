using Dominio.Entidades.Cadastros;
using System.Collections.Generic;

namespace Infra.Interfaces.Cadastros
{
    public interface IJogoRepositorio
    {
        IEnumerable<Jogo> Listar(int? codigo, string nome, int? codigoUsuarioDono);
        Jogo Recuperar(int codigo);
        Jogo Inserir(string nome, int codigoUsuarioDono);
        Jogo Atualizar(int codigo, string nome);
        bool Excluir(int codigo);
        bool Devolver(int codigoJogo);
        bool Emprestar(int codigoUsuarioEmprestimo, int codigoJogo);
    }
}