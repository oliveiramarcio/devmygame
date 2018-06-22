using System;
using System.Net;
using System.Net.Mail;

namespace Biblioteca.Classes
{
    public class EnviaEmail
    {
        //Exemplo para enviar email:
        //EnviaEmail enviaEmail = new EnviaEmail();
        //enviaEmail.Email("rone_soares@yahoo.com.br", "teste", "Mensagem de <b>teste</b>!", true);

        public void Email(string destinatario, string assunto, string mensagem, bool mensagemHtml)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(Constantes.Email.smtp);

                mail.From = new MailAddress(Constantes.Email.remetente);
                mail.To.Add(destinatario);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = mensagemHtml;

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(Constantes.Email.remetente, Constantes.Email.senhaRemetente);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao enviar e-mail: " + ex);
            }
        }
    }
}
