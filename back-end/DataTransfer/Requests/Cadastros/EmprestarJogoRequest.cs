namespace DataTransfer.Requests.Cadastros
{
    public class EmprestarJogoRequest
    {
        public int CodigoUsuarioEmprestimo { get; set; }
        public int[] CodigosJogos { get; set; }
    }
}