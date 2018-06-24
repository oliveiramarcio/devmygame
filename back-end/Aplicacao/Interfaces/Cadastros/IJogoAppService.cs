using DataTransfer.Requests.Cadastros;
using DataTransfer.Responses.Cadastros;
using System.Collections.Generic;

namespace Aplicacao.Interfaces.Cadastros
{
    public interface IJogoAppService
    {
        IEnumerable<JogoResponse> Listar(ListarJogoRequest request);
        JogoResponse Recuperar(int codigo);
        JogoResponse Inserir(InserirJogoRequest request);
        JogoResponse Atualizar(AtualizarJogoRequest request);
        bool Excluir(int[] codigos);
        bool Devolver(int[] codigosJogos);
        bool Emprestar(EmprestarJogoRequest request);
    }
}