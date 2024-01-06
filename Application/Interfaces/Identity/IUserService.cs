using Application.Models.Identity;

namespace Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ReadAllAsync();

        Task<User> ReadByIdAsync(string id);
    }
}
