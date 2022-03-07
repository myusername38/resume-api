using System;
using martin_resume_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace martin_resume_api.Repos
{
    public interface IUserRepo : IDisposable
    {
        DbSet<User> Users();
        Task<int> Save();
    }

    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<User> Users()
        {
            return _dbContext.Users;
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

