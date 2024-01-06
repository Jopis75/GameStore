﻿namespace Application.Models.Identity
{
    public class AuthenticationResponse
    {
        public string? Id { get; set; }

        public string? JwtSecurityToken { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }
    }
}
