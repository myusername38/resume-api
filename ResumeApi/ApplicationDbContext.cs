using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ResumeApi.Models;

namespace ResumeApi
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base (options)
		{
		}

		public DbSet<User> Users { get; set; } = null!;

		public DbSet<Solution> Solutions { get; set; } = null!;

		public DbSet<UserSolution> UserSolutions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Solution>().ToTable("Solution");
            modelBuilder.Entity<UserSolution>().ToTable("UserSolution");

            modelBuilder.Entity<UserSolution>().HasKey(x => new { x.SolutionId, x.UserId });

            modelBuilder.Entity<UserSolution>()
                .HasOne(u => u.User)
                .WithMany(u => u.LikedSolutions)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserSolution>()
                .HasOne(s => s.Solution)
                .WithMany(s => s.Likes)
                .HasForeignKey(us => us.SolutionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Solutions)
                .WithOne(s => s.Author)
                .HasForeignKey(s => s.AuthorId);

            modelBuilder.Entity<Solution>()
                .HasOne(s => s.Author)
                .WithMany(u => u.Solutions)
                .HasForeignKey(s => s.AuthorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}