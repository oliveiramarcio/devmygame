using Dominio.Entidades.Cadastros;
using System.Collections.Generic;

namespace Infra.Interfaces.Cadastros
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<Usuario> Listar(int? codigo, string email, string senha);
        Usuario RecuperarPorCodigo(int codigo);
        Usuario RecuperarPorEmail(string email);
        Usuario Inserir(string nome, string email, string senha, string telefone);
        Usuario Atualizar(int codigo, string nome, string senha, string telefone);
        bool Excluir(int codigo);
        bool AutenticarUsuario(string email, string senha);
    }
}