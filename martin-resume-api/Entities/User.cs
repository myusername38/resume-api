using System;
using System.ComponentModel.DataAnnotations;

namespace martin_resume_api.Entities
{
	public class User
	{
		public int Id { get; set; }

		public string Email { get; set; }

		public UserRole Role { get; set; }

		public string Password { get; set; }

		public Boolean EmailList { get; set; }

		public virtual ICollection<Solution>? Solutions { get; set; }

		public virtual ICollection<Solution>? FollowedSolutions { get; set; }

	}

	public enum UserRole
	{
		Owner = 0,
		Follower = 1
	}

	/*
	public class UserSolution
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User USer { get; set; }
        public int SolutionId { get; set; }
    }
	*/
}

