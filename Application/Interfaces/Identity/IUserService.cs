using Application.Dtos.Identity;

namespace Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> ReadAllAsync();

        Task<UserDto> ReadByIdAsync(string id);
    }
}
