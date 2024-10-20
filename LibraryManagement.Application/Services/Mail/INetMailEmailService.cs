using System.Net.Mail;

namespace LibraryManagement.Application.Services.Mail
{
    public interface INetMailEmailService
    {
        void SendEMail(string name, string mailTo, string delayDays, string livro);
    }
}
