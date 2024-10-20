using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace LibraryManagement.Application.Services.Mail
{
    public class MailKitEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string EMAIL_HOST;
        private readonly string EMAIL_ORIGEM;
        private readonly string EMAIL_SENHA;

        public MailKitEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            EMAIL_HOST = _configuration.GetSection("emailServico").Value;
            EMAIL_ORIGEM = _configuration.GetSection("emailServico").Value;
            EMAIL_SENHA = _configuration.GetSection("emailServico").Value;
        }
        public async Task EnviarEmail(string nomeRemetente, string emailRemetente, string nomeDestinario, string emailDestinario, string mensagem)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(EMAIL_ORIGEM, emailRemetente));

            message.Subject = "Lembre: sua devolução de emprestimo está atrasada";

            message.To.Add(new MailboxAddress(nomeDestinario, emailDestinario));

            //body
            message.Body = new TextPart("html")
            {
                Text = mensagem,
            };

            using (var client = new SmtpClient())
            {
                client.Connect(EMAIL_HOST, 587, true);

                client.Authenticate(emailRemetente, EMAIL_SENHA);

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
