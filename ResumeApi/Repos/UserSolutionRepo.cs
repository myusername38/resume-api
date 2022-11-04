using System;
using Microsoft.EntityFrameworkCore;
using ResumeApi.Models;

namespace ResumeApi.Repos
{
    public interface IUserSolutionRepo : IDisposable
    {
        DbSet<UserSolution> UserSolutions();
        Task<int> Save();
    }

    public class UserSolutionRepo : IUserSolutionRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

        public UserSolutionRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<UserSolution> UserSolutions()
        {
            return _dbContext.UserSolutions;
        }

        public Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

