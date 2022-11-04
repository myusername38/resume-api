using System;
using Microsoft.EntityFrameworkCore;
using ResumeApi.Models;

namespace ResumeApi.Repos
{
	public interface ISolutionRepo : IDisposable
	{
		DbSet<Solution> Solutions();
		Task<int> Save();
	}

	public class SolutionRepo : ISolutionRepo
	{
		private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

        public SolutionRepo(ApplicationDbContext dbContext) 
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

