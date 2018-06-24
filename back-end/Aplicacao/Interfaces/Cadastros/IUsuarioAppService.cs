using DataTransfer.Requests.Cadastros;
using DataTransfer.Responses.Cadastros;
using System.Collections.Generic;

namespace Aplicacao.Interfaces.Cadastros
{
    public interface IUsuarioAppService
    {
        IEnumerable<UsuarioResponse> Listar(ListarUsuarioRequest request);
        UsuarioResponse RecuperarPorCodigo(int codigo);
        UsuarioResponse RecuperarPorEmail(string email);
        UsuarioResponse Inserir(InserirUsuarioRequest request);
        UsuarioResponse Atualizar(AtualizarUsuarioRequest request);
        bool Excluir(int codigo);
        bool AutenticarUsuario(string email, string senha);
    }
}