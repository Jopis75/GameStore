using Application.Models.Identity;

namespace Application.Dtos.Identity
{
    public class RegistrationRequestDto
    {
        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public Roles Role { get; set; }
    }
}
