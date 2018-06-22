using System;

namespace Biblioteca.Excecoes
{
    public class RequestInvalidoExcecao : Exception
    {
        public RequestInvalidoExcecao(string mensagem = "Request inválido")
            : base(mensagem)
        { }
    }
}