using System;

namespace Biblioteca.Excecoes
{
    public class RegraDeNegocioExcecao : Exception
    {
        public RegraDeNegocioExcecao(string mensagem)
            : base(mensagem)
        { }
    }
}