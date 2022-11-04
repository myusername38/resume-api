using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeApi.Dtos.User;
using ResumeApi.Models;
using ResumeApi.Services;
using System.ComponentModel.DataAnnotations;

namespace ResumeApi.Controllers
{
	[ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly ISolutionService _solutionService;

        public UserController(
            IUserService userService,
            ISolutionService solutionService,
            ILikeService likeService
        )
        {
            _userService = userService;
            _solutionService = solutionService;
            _likeService = likeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [Route("like-solution")]
        [HttpPut]
        public async Task<IActionResult> LikeSolution(
           [FromBody]
           ToggleLikeSolutionDto body
       )
        {
            var user = await _userService.GetUser(body.userId);
            var solution = await _solutionService.GetSolution(body.solutionId);
            await _likeService.LikeSolution(user, solution);
            return Ok(user);
        }

        [Route("unlike-solution")]
        [HttpPut]
        public async Task<IActionResult> UnlikeSolution(
          [FromBody]
           ToggleLikeSolutionDto body
      )
        {
            var user = await _userService.GetUser(body.userId);
            var solution = await _solutionService.GetSolution(body.solutionId);
            await _likeService.UnlikeSolution(user, solution);
            return Ok(user);
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
        /*
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
        */
    }
}

