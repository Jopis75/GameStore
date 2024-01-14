using Application.Dtos.Common;

namespace Application.Dtos.Identity
{
    public class ReadUserByUserIdRequestDto : ReadRequestDto
    {
        public string? UserId { get; set; }
    }
}
