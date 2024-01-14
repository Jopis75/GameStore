using Application.Dtos.Common;

namespace Application.Dtos.Identity
{
    public class ReadUserResponseDto : ResponseDto
    {
        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
