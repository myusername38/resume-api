using System;
using martin_resume_api.Entities;
using martin_resume_api.Repos;
using Microsoft.EntityFrameworkCore;

namespace martin_resume_api.Services
{
	public interface IUserService
    {
		Task<List<User>> GetUsers(int skip = 0, int take = 100);
		Task<User> CreateUser(User user, string password);
		Task DeleteUser(int id);
	}

	public class UserService : IUserService
	{
		private readonly UserRepo _userRepo;

		public UserService(UserRepo userRepo)
		{
			_userRepo = userRepo;
		}

		public async Task<List<User>> GetUsers(int skip = 0, int take = 100)
        {
			var users = await _userRepo.Users()
				.AsNoTracking()
				.Skip(skip)
				.Take(take)
				.ToListAsync();
			return users;
        }

		public async Task<User> CreateUser(User user, string password)
		{
			var existingUser = await _userRepo.Users()
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Email == user.Email);

			if (existingUser != null)
            {
				return null;
            }

			var encryptedPassword = Encryption.HashPassword(user.Email, password);
			user.Password = encryptedPassword;
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

