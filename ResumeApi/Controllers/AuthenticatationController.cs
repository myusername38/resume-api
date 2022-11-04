using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ResumeApi.Auth;
using ResumeApi.Dtos.User;
using ResumeApi.Models;
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

        public AuthenticationController(IAuthService authService, IUserService userService, IConfiguration configuration)
        {
            this._authService = authService;
            this._userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<User> Register([FromBody] CreateUserDto dto)
        {
            var user = await _authService.Register(dto.Email, dto.UserName, dto.Password);
            var userData = new User
            {
                Role = dto.Role,
                Email = dto.Email,
                UserName = dto.UserName,
                EmailList = dto.EmailList,
            };
            await _userService.CreateUser(userData);
            return userData;

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
    }
}

