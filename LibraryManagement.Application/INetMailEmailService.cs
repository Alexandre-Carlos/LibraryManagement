using System.Net.Mail;

namespace LibraryManagement.Infrastructure.Services.Mail
{
    public interface INetMailEmailService
    {
        void SendEMail(string name, string mailTo, string delayDays);
    }
}
