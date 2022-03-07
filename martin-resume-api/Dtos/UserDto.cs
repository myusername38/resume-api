using System;
using System.ComponentModel.DataAnnotations;

namespace martin_resume_api.Dtos
{
	public class CreateUserDto
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public Boolean EmailList { get; set; }
	}

	public class UserDto
    {
		public int Id { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Role { get; set; }

		[Required]
		public Boolean EmailList { get; set; }
    }

	public enum UserRole
	{
		Owner = 0,
		Follower = 1
	}
}

