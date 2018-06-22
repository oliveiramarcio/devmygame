namespace DataTransfer.Requests.Cadastros
{
    public class InserirUsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
    }
}