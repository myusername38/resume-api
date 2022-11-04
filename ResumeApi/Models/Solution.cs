using System;
using ResumeApi.Enums;

namespace ResumeApi.Models
{
	public class Solution
	{
		public int Id { get; set; }

		public int AuthorId { get; set; }

		public virtual User Author { get; set; }

		public string ProgramData { get; set; }

		public string ProgramDescription { get; set; }

		public string ProgramTitle { get; set; }

		public string TestCases { get; set; }

		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }

		public Visibility Visibility { get; set; }

        public virtual List<UserSolution> Likes { get; set; }
    }
}

