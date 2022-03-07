using System.Diagnostics.CodeAnalysis;
using martin_resume_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace martin_resume_api
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(x =>
			{
				x.Property(x => x.Password).HasColumnType(
					"nvarchar(2048)"
				).IsRequired(); // non nullable type EF core makes it nullable by default with strings
								// default nvarchar max (uses up to much space)
				x.HasMany(y => y.Solutions)
				.WithOne(y => y.Author)
				.HasForeignKey(y => y.AuthorId);
				
			});

			modelBuilder.Entity<Solution>(x =>
			{
				x.HasOne(y => y.Author)
				.WithMany(y => y.Solutions)
				.HasForeignKey(y => y.AuthorId)
				.OnDelete(DeleteBehavior.Restrict);

				x.HasMany(y => y.Followers)
				.WithMany(y => y.FollowedSolutions);
			});

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Solution> Solutions { get; set; }
	}
}


