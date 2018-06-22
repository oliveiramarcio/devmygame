using System;

namespace Biblioteca.Excecoes
{
    public class ParametroInvalidoExcecao : Exception
    {
        public ParametroInvalidoExcecao(string mensagem)
            : base(mensagem)
        { }
    }
}