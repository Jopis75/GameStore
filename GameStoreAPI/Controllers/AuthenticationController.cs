﻿using Application.Interfaces.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = await _authenticationService.Login(authenticationRequest);
            return Ok(authenticationResponse);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest registrationRequest)
        {
            var registrationResponse = await _authenticationService.Register(registrationRequest);
            return Ok(registrationResponse);
        }
    }
}
