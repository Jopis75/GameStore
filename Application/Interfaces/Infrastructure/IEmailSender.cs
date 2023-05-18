using Application.Models.Email;

namespace Application.Interfaces.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
