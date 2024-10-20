using LibraryManagement.Application.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LibraryManagement.Application.Services.Mail
{
    public class NetMailEmailService : INetMailEmailService
    {
        #region Private Fields

        private readonly string EMAIL_HOST;
        private readonly string EMAIL_ORIGEM;
        private readonly string EMAIL_SENHA;
        private readonly ILogger<NetMailEmailService> _logger;

        #endregion Private Fields
        public NetMailEmailService(ILogger<NetMailEmailService> logger, ApplicationConfig appConfig)
        {
            EMAIL_HOST = appConfig.MailServiceConfig.MailHost;
            EMAIL_ORIGEM = appConfig.MailServiceConfig.MailFrom;
            EMAIL_SENHA = appConfig.MailServiceConfig.MailSecret;
            _logger = logger;

        }

        public void SendEMail(string name, string mailTo, string delayDays, string livro)
        {
            try
            {
                using (var mensagemEmail = new MailMessage())
                {
                    mensagemEmail.From = new MailAddress(EMAIL_ORIGEM);
                    mensagemEmail.To.Add(new MailAddress(mailTo));
                    var body = TemplateMail.TemplateNotification;
                    body = body.Replace("#ReceiverName#", name);
                    body = body.Replace("#Message#", "Sua devolução do livro está em atraso!");
                    body = body.Replace("#Description#", "Estamos a disposição para atende-lo!");
                    body = body.Replace("#Livro#", $"Aguardamos a devolução do {livro}!");



                    mensagemEmail.Subject = $"Olá {name}, sua devolução do livro está em atraso!";
                    mensagemEmail.IsBodyHtml = true ;
                    mensagemEmail.Body = body;

                    mensagemEmail.BodyEncoding = Encoding.UTF8;
                    mensagemEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                    mensagemEmail.Priority = MailPriority.Normal;

                    using (var smtpCliente = new SmtpClient())
                    {
                        smtpCliente.Host = EMAIL_HOST;
                        smtpCliente.Port = 587;
                        smtpCliente.EnableSsl = true;
                        smtpCliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpCliente.Credentials = new NetworkCredential(EMAIL_ORIGEM, EMAIL_SENHA);
                        smtpCliente.UseDefaultCredentials = false;

                        smtpCliente.Send(mensagemEmail);
                    }
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                _logger.LogInformation("Mensagem: ", ex.Message);
                return;
            }
            catch (SmtpException ex)
            {
                _logger.LogInformation("Mensagem SMPT Fail: ", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Mensagem Exception: ", ex.Message);
                return;
            }

            _logger.LogInformation($"Email enviado com sucesso! Destinatário: { mailTo}");
        }
    }
}
