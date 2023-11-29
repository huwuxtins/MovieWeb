using MovieWeb.Models;

namespace MovieWeb.Services.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(MailData mailData);
    }
}
