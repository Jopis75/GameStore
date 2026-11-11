using GameStore.Application.Models.Email;

namespace GameStore.Application.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
