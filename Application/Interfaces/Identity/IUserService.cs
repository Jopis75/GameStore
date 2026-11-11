using GameStore.Application.Dtos.General;
using GameStore.Application.Dtos.Identity;

namespace GameStore.Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<HttpResponseDto<UserDto>> ReadAllAsync();

        Task<HttpResponseDto<UserDto>> ReadByIdAsync(string id);
    }
}
