using System;
namespace ResumeApi.Models
{
	public class UserSolution
	{
		public int Id { get; set; }

		public Boolean Notifications { get; set; }

		public DateTime DateLiked { get; set; }

		public int UserId { get; set; }

		public virtual User User { get; set; }

		public int SolutionId { get; set; }

		public virtual Solution Solution { get; set; }
	}
}

