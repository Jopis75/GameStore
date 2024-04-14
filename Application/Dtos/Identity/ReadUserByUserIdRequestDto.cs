using Application.Dtos.Common;

namespace Application.Dtos.Identity
{
    public class ReadUserByUserIdRequestDto : RequestDto
    {
        public string? UserId { get; set; }
    }
}
