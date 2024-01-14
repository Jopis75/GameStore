using Microsoft.AspNetCore.Identity;

namespace Application.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
