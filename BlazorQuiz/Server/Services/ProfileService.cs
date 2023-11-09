using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorQuiz.Server.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<QuizModel>> GetUserCreatedGamesAsync(string userId)
        {

            var userQuizzes = _context.Quizzes.Where(q => q.UserRefId == userId).ToList();

            return userQuizzes;

        }

        public async Task<List<UserQuizModel>> GetUserGamesAsync(string userId)
        {
            var userGames = _context.UserQuizModels.Where(q => q.UserRefId == userId).ToList();

            return userGames;
        }
    }
}
