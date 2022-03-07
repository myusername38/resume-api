using System;
using martin_resume_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace martin_resume_api.Repos
{
	public interface IUserSolutionRepo : IDisposable
	{
		DbSet<Solution> Solutions();
		Task<int> Save();
	}

	public class UserSolutionRepo : IUserSolutionRepo
	{
        private readonly ApplicationDbContext _dbContext;

        public UserSolutionRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<Solution> Solutions()
        {
            return _dbContext.Solutions;
        }


        public Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

