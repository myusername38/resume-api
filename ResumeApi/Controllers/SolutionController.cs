using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeApi.Dtos.Solution;
using ResumeApi.Services;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ResumeApi.Models;

namespace ResumeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService _solutionService;
        private readonly IMapper _mapper;

        public SolutionController(
            ISolutionService solutionService,
            IMapper mapper
        )
        {
            _solutionService = solutionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSolutions()
        {
            var Solutions = await _solutionService.GetSolutions();
            return Ok(Solutions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSolution(
            [FromBody]
            CreateSolutionDto body
        )
        {
            var solution = await this._solutionService.CreateNewSolution(body);
            return Ok(solution);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolution(
            [FromRoute]
            [Required]
            int SolutionId
        )
        {
            await _solutionService.DeleteSolution(SolutionId);
            return Ok();
        }
        /*
        [HttpPost("Login")]
        public async Task<IActionResult> Login(
            [FromBody]
            LoginDto body
        )
        {
            var Solution = await _authService.Authenticate(body.Email, body.Password);
            if (Solution == null)
            {
                return Unauthorized("Unauthorized");
            }
            var token = _authService.GenerateJWTToken(Solution);

            return Ok(new LoginTokenDto
            {
                Token = token
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            [FromBody]
            CreateSolutionDto body
        )
        {
            var Solution = new Solution
            {
                Role = 0,
                Email = body.Email,
                Password = body.Password,
                EmailList = body.EmailList
            };
            var createdSolution = await _SolutionService.CreateSolution(Solution, body.Password);

            if (createdSolution == null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(createdSolution);
        }
        */
    }
}

