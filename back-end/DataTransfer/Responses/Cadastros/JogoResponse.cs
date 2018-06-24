using System;

namespace DataTransfer.Responses.Cadastros
{
    public class JogoResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string DataCadastro { get; set; }
        public int CodigoUsuarioDono { get; set; }
        public string NomeUsuarioEmpresitmo { get; set; }
        public string DataEmprestimo { get; set; }
        public int QuantidadeDiasEmprestimo { get; set; }
    }
}