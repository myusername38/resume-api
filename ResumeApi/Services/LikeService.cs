using System;
using Microsoft.EntityFrameworkCore;
using ResumeApi.Models;
using ResumeApi.Repos;

namespace ResumeApi.Services
{
	public interface ILikeService
	{
        Task LikeSolution(User user, Solution solution);
        Task UnlikeSolution(User user, Solution solution);
        Task<List<Solution>> GetUserLikeSolutions(User user, int skip = 0, int take = 100);
    }

    public class LikeService : ILikeService
    {
        private readonly UserSolutionRepo _userSolutionRepo;

        public LikeService(UserSolutionRepo userSolutionRepo)
        {
            _userSolutionRepo = userSolutionRepo;
        }

        public async Task<List<Solution>> GetUserLikeSolutions(User user, int skip = 0, int take = 100)
        {
            return await _userSolutionRepo.UserSolutions()
                    .Include(x => x.Solution)
                    .Where(x => x.UserId == user.Id)
                    .OrderByDescending(x => x.DateLiked)
                    .Select(x => x.Solution)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
        }

        public async Task LikeSolution(User user, Solution solution)
        {
            var userSolution = await _userSolutionRepo.UserSolutions().FirstOrDefaultAsync(x => x.UserId == user.Id && x.SolutionId == solution.Id);
            if (userSolution != null)
            {
                return;
            }
            userSolution = new UserSolution
            {
                Notifications = false,
                DateLiked = DateTime.Now,
                UserId = user.Id,
                SolutionId = solution.Id,
            };
            _userSolutionRepo.UserSolutions().Add(userSolution);
            await _userSolutionRepo.Save();
            return;
        }

        public async Task UnlikeSolution(User user, Solution solution)
        {
            var userSolution = await _userSolutionRepo.UserSolutions().FirstOrDefaultAsync(x => x.UserId == user.Id && x.SolutionId == solution.Id);
            if (userSolution == null)
            {
                return;
            }
            _userSolutionRepo.UserSolutions().Remove(userSolution);
            await _userSolutionRepo.Save();
            return;
        }
    }
}

