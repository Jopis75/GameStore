using Application.Dtos.Common;

namespace Application.Dtos.Identity
{
    public class RegistrationResponseDto : ResponseDto
    {
        public string? UserId { get; set; }
    }
}
