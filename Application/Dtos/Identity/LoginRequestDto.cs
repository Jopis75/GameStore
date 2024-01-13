using Application.Dtos.Common;

namespace Application.Dtos.Identity
{
    public class LoginRequestDto : RequestDto
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
