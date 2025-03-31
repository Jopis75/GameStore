namespace Application.Dtos.Identity
{
    public class LoginRequestDto
    {
        public bool LockoutOnFailure { get; set; }

        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
