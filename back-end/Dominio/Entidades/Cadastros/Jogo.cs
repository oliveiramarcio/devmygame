using System;

namespace Dominio.Entidades.Cadastros
{
    public class Jogo
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CodigoUsuarioDono { get; set; }
        public string NomeUsuarioEmpresitmo { get; set; }
        public DateTime? DataEmprestimo { get; set; }

        public int QuantidadeDiasEmprestimo
        {
            get => (this.DataEmprestimo == null) ? 0 : Convert.ToInt32((DateTime.Now - (DateTime)this.DataEmprestimo).TotalDays);
        }

        public Jogo()
        {

        }
    }
}