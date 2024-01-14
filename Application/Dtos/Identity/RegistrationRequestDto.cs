using Application.Dtos.Common;
using Application.Models.Identity;

namespace Application.Dtos.Identity
{
    public class RegistrationRequestDto : RequestDto
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Roles? Role { get; set; }
    }
}
