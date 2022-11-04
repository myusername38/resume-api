using System;
using ResumeApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeApi.Dtos.User
{
    public class ToggleLikeSolutionDto
    {
        [Required]
        public int userId { get; set; }

        [Required]
        public int solutionId { get; set; }
    }
}

