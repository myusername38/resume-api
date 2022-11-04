using System;
using ResumeApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeApi.Dtos.User
{
	public class LoginDto
	{
        [Required(ErrorMessage = "User Name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Password { get; set; }
    }
}

