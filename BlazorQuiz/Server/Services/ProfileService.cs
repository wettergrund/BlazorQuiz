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

        // [HttpGet("mygames")]
        public async Task<List<UserCreatedQuizViewModel>> GetUserGamesAsync(string userId)
        {
            // Get all quizzes on active user
            var userGames = _context.Quizzes.Where(q => q.UserRefId == userId).ToList();

            var viewModels = new List<UserCreatedQuizViewModel>();
            // Set name and publicid of quiz and add to list
            foreach (var userGame in userGames)
            {
                var viewModel = new UserCreatedQuizViewModel();
                viewModel.Name = userGame.Name;
                viewModel.PublicId = userGame.PublicId;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        // [HttpGet("myquizzes")]
        public async Task<List<QuizModel>> GetUserCreatedGamesAsync(string userId)
        {

            var userQuizzes = _context.Quizzes.Where(q => q.UserRefId == userId).ToList();

            return userQuizzes;

        }

        // [HttpGet("myquizzes/{publicid}")]
        public async Task<List<UserQuizViewModel>> GetDataOnGameAsync(string publicId, string username)
        {
            // Get userquizmodels on quizId
            var dataGames = _context.UserQuizModels.Where(q => q.QuizRefPublicId == publicId).ToList();
            var viewModels = new List<UserQuizViewModel>();

            //Set data to viewmodel of score and name for user on this quiz to list.
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
