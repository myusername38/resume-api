using System;
using System.ComponentModel.DataAnnotations;
using martin_resume_api.Auth;
using martin_resume_api.Dtos;
using martin_resume_api.Entities;
using martin_resume_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace martin_resume_api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[AllowAnonymous]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly AuthService _authService;

		public UserController(
			IUserService userService,
			AuthService authService
		)
		{
			_userService = userService;
			_authService = authService;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
        {
			var users = await _userService.GetUsers();
			return Ok(users);
        }

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(
			[FromRoute]
			[Required]
			int userId
		)
        {
			await _userService.DeleteUser(userId);
			return Ok();
        }

		[HttpPost("Login")]
		public async Task<IActionResult> Login(
			[FromBody]
			LoginDto body
		)
        {
			var user = await _authService.Authenticate(body.Email, body.Password);
			if (user == null)
            {
				return Unauthorized("Unauthorized");
            }
			var token = _authService.GenerateJWTToken(user);

			return Ok(new LoginTokenDto
			{
				Token = token
			});
        }

		[HttpPost("Register")]
		public async Task<IActionResult> Register(
			[FromBody]
			CreateUserDto body
		)
        {
			var user = new User
			{
				Role = 0,
				Email = body.Email,
				Password = body.Password,
				EmailList = body.EmailList
			};
			var createdUser = await _userService.CreateUser(user, body.Password);

			if (createdUser == null)
            {
				return BadRequest("Something went wrong");
            }

			return Ok(createdUser);
        }
	}
}

