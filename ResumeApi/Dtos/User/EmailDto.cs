using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeApi.Dtos.User
{
	public class EmailDto
	{
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}

