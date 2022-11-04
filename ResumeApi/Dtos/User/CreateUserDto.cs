using System;
using System.ComponentModel.DataAnnotations;
using ResumeApi.Enums;

namespace ResumeApi.Dtos.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required]
        public Boolean EmailList { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}

