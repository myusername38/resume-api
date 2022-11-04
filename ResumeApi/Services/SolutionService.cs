using System;
using Microsoft.EntityFrameworkCore;
using ResumeApi.Dtos.Solution;
using ResumeApi.Models;
using ResumeApi.Repos;

namespace ResumeApi.Services
{
    public interface ISolutionService
    {
        Task<List<Solution>> GetSolutions(int skip = 0, int take = 100);
        Task<Solution> GetSolution(int id);
        Task<Solution> CreateNewSolution(CreateSolutionDto createSolutionDto);
        
        Task<Solution> CreateSolution(Solution solution);
        Task DeleteSolution(int id);
    }

    public class SolutionService : ISolutionService
    {

        private readonly SolutionRepo _solutionRepo;

        public SolutionService(SolutionRepo solutionRepo) {
            _solutionRepo = solutionRepo;
        }

        public async Task<Solution> GetSolution(int id)
        {
            return await _solutionRepo.Solutions().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Solution> CreateSolution(Solution solution)
        {
            _solutionRepo.Solutions().Add(solution);
            await _solutionRepo.Save();
            return solution;
        }

        public async Task<Solution> CreateNewSolution(CreateSolutionDto createSolutionDto)
        {
            var solution = new Solution
            {
                AuthorId = createSolutionDto.AuthorId,
                ProgramData = createSolutionDto.ProgramData,
                ProgramDescription = createSolutionDto.ProgramDescription,
                ProgramTitle = createSolutionDto.ProgramTitle,
                TestCases = createSolutionDto.TestCases,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Visibility = Enums.Visibility.Visible,
            };
            return await this.CreateSolution(solution);
        }

        public async Task DeleteSolution(int id)
        {
            var solution = await _solutionRepo.Solutions()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (solution != null)
            {
                _solutionRepo.Solutions().Remove(solution);
                await _solutionRepo.Save();
            }
            return;
        }

        public async Task<List<Solution>> GetSolutions(int skip = 0, int take = 100)
        {
            var solutions = await _solutionRepo.Solutions()
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return solutions;
        }

        public async Task<List<Solution>> GetUserSolutions(int userId, int skip = 0, int take = 100)
        {
            var solutions = await _solutionRepo.Solutions()
               .AsNoTracking()
               .Skip(skip)
               .Take(take)
               .Where(x => x.AuthorId == userId)
               .ToListAsync();
            return solutions;
        }

        public async Task<List<Solution>> GetUserFollowedSolutions(User user, int skip = 0, int take = 100)
        {
            var solutions = await _solutionRepo.Solutions()
               .AsNoTracking()
               .Skip(skip)
               .Take(take)
               .ToListAsync();
            return solutions;
        }
    }
}

