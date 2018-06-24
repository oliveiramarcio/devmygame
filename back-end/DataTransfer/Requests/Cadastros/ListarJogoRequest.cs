namespace DataTransfer.Requests.Cadastros
{
    public class ListarJogoRequest
    {
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public int? CodigoUsuarioDono { get; set; }
    }
}