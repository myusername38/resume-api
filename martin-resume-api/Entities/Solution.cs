using System;
using System.ComponentModel.DataAnnotations;

namespace martin_resume_api.Entities
{
	public class Solution
	{
		public int Id { get; set; }

		public int AuthorId { get; set; }

		public virtual User Author { get; set; }

		public string ProgramData { get; set; }

		public string ProgramDescription { get; set; }

		public string TestCases { get; set; }

		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }

		public Visibility Visibility { get; set; }

		public virtual ICollection<User> Followers { get; set; }

	}


	public enum Visibility
	{
		Visible = 0,
		hidden = 1
	}

}

