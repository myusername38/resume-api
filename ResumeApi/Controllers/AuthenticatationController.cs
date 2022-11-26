using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Pkcs;
using ResumeApi.Auth;
using ResumeApi.Dtos.User;
using ResumeApi.Models;
using ResumeApi.Models.Email;
using ResumeApi.Services;

namespace ResumeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;

        public AuthenticationController(
            IAuthService authService,
            IUserService userService,
            IConfiguration configuration,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService
        ) {
            this._authService = authService;
            this._userService = userService;
            this._emailService = emailService;
            this._emailTemplateService = emailTemplateService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<string> Register([FromBody] CreateUserDto dto)
        {
            var user = await _authService.Register(dto.Email, dto.UserName, dto.Password);
            var userData = new User
            {
                Role = dto.Role,
                Email = dto.Email,
                UserName = dto.UserName,
                EmailList = dto.EmailList,
            };
            var applicationUser = await _userService.CreateUser(userData);
            var code = await _authService.GetEamilVerificationCode(user);
            var email = _emailTemplateService.GenerateSendEmailLink("localhost:4200", "martinjsnomail@gmail.com", "test", "test");
   
            MailData mailData = new MailData(
            new List<string> { "martin@skismolka.com" },
            "Welcome to the MailKit Demo",
            email);

            await _emailService.SendAsync(mailData, new CancellationToken());
            return code;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authService.Authenticate(dto.Email, dto.Password);
            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }
            var userData = await _userService.GetUserByEmail(dto.Email);
            var jwt = _authService.GenerateJWTToken(userData);
            return Ok(jwt);
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] EmailDto dto)
        {

            // var code = await _authService.GetChangePasswordCode()
            return Ok("");
        }
    }
}

