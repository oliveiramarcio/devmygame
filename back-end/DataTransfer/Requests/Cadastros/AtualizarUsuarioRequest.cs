﻿namespace DataTransfer.Requests.Cadastros
{
    public class AtualizarUsuarioRequest
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
    }
}