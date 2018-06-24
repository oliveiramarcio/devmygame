using System;

namespace Dominio.Entidades.Cadastros
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }

        public Usuario()
        {

        }
    }
}