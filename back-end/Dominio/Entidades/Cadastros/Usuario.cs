using System;

namespace Dominio.Entidades.Cadastros
{
    public class Usuario
    {
        public int Codigo { get; protected set; }
        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string Senha { get; protected set; }
        public string Telefone { get; protected set; }
        public DateTime DataCadastro { get; protected set; }
    }
}