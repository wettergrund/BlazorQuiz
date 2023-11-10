using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public interface IGameService
    {
        Task<GameWithQuizViewModel> GetUserQuizAsync(int id);
        Task<QuizModel> CreateQuizAsync(string title, List<NewQuestionViewModel> questions, int seconds, string userId);
        Task<UserQuizModel> CreateNewGameAsync(int quizId, string userId);
        Task<bool> UpdateGameAsync(int gameId, string gameState);


    }
}