using System;
using ResumeApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace ResumeApi.Dtos.Solution
{
	public class CreateSolutionDto
	{
        [Required]
        public int AuthorId { get; set; }

        [Required]
        public string ProgramData { get; set; }

        [Required]
        public string ProgramDescription { get; set; }

        [Required]
        public string ProgramTitle { get; set; }

        [Required]
        public string TestCases { get; set; }
    }
}

