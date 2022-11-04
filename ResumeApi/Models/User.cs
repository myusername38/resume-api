using System;
using ResumeApi.Enums;

namespace ResumeApi.Models
{
	public class User
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public UserRole Role { get; set; }

		public Boolean EmailList { get; set; }

		public virtual ICollection<Solution> Solutions { get; set; }

        public virtual ICollection<UserSolution> LikedSolutions { get; set; }
	}
}

