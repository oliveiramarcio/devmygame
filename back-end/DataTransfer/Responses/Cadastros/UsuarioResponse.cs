using System;

namespace DataTransfer.Responses.Cadastros
{
    public class UsuarioResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string DataCadastro { get; set; }
    }
}