using System;
using Microsoft.EntityFrameworkCore;
using ResumeApi.Models;

namespace ResumeApi.Repos
{
    public interface IUserRepo : IDisposable
    {
        DbSet<User> Users();
        Task<int> Save();
    }

    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

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

        public Task<int> Update(User user)
        {
            _dbContext.Update(user);
            return this.Save();
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

