using Application.Dtos.Common;
using Application.Dtos.Identity;

namespace Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<HttpResponseDto<ReadUserResponseDto>> ReadAllAsync(ReadUserAllRequestDto readUserAllRequestDto);

        Task<HttpResponseDto<ReadUserResponseDto>> ReadByIdAsync(ReadUserByUserIdRequestDto readUserByUserIdRequestDto);
    }
}
