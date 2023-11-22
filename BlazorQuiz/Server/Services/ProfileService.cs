using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;

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

        public async Task<List<UserCreatedQuizViewModel>> GetUserGamesAsync(string userId)
        {
            var userGames = _context.Quizzes.Where(q => q.UserRefId == userId).ToList();

            var viewModels = new List<UserCreatedQuizViewModel>();

            foreach (var userGame in userGames)
            {
                var viewModel = new UserCreatedQuizViewModel();
                viewModel.Name = userGame.Name;
                viewModel.PublicId = userGame.PublicId;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public async Task<List<UserQuizViewModel>> GetDataOnGameAsync(string publicId, string username)
        {
            var dataGames = _context.UserQuizModels.Where(q => q.QuizRefPublicId == publicId).ToList();
            var viewModels = new List<UserQuizViewModel>();

            foreach (var dataGame in dataGames)
            {
                var viewModel = new UserQuizViewModel();
                viewModel.Score = dataGame.Score;
                viewModel.User = _context.Users
                    .Where(x => x.Id == dataGame.UserRefId)
                    .Select(x => x.UserName).FirstOrDefault();
                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
