using Application.Dtos.Common;

namespace Application.Dtos.Identity
{
    public class LoginResponseDto : ResponseDto
    {
        public string? UserId { get; set; }

        public string? JwtSecurityToken { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }
    }
}
