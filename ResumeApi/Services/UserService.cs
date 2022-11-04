using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResumeApi.Models;
using ResumeApi.Repos;

namespace ResumeApi.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(int skip = 0, int take = 100);
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> GetUser(int id);
        Task DeleteUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly UserRepo _userRepo;

        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _userRepo.Users().FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepo.Users().FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<List<User>> GetUsers(int skip = 0, int take = 100)
        {
            var users = await _userRepo.Users()
                .Include(user => user.Solutions)
                .Include(user => user.LikedSolutions)
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return users;
        }


        public async Task<User> CreateUser(User user)
        {
            _userRepo.Users().Add(user);
            await _userRepo.Save();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _userRepo.Users()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                _userRepo.Users().Remove(user);
                await _userRepo.Save();
            }
            return;
        }
    }
}

