using System;
using System.ComponentModel.DataAnnotations;

namespace martin_resume_api.Dtos
{
	public class LoginDto
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}

	public class LoginTokenDto
    {
		public string Token { get; set; }
    }
}

