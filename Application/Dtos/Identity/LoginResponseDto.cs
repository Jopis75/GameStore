namespace Application.Dtos.Identity
{
    public class LoginResponseDto
    {
        public string UserId { get; set; } = default!;

        public string JwtSecurityToken { get; set; } = default!;

        public string? UserName { get; set; }

        public string? Email { get; set; }
    }
}
