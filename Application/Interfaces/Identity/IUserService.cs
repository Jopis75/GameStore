using Application.Dtos.General;
using Application.Dtos.Identity;

namespace Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<HttpResponseDto<UserDto>> ReadAllAsync();

        Task<HttpResponseDto<UserDto>> ReadByIdAsync(string id);
    }
}
