using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;
using BlazorQuiz.Shared.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public interface IGameService
    {
        Task<GameWithQuizViewModel> GetQuizByPublicIdAsync(string id);
        Task<QuizModel> CreateQuizAsync(string title, List<NewQuestionViewModel> questions, int seconds, string userId);
        Task<QuizViewModel> CreateNewGameAsync(string quizId, string userId);
        Task<bool> UpdateGameAsync(int gameId, string gameState);
        
        Task<bool> CheckGuess(GuessCheckViewModel guess);

        Task<UserQuizModel> FinishedGame(UserQuizModel quiz, List<GuessCheckViewModel> guesses);
        
        UserQuizModel FindUserQuiz(int id);
        List<QuestionModel> FindQuestionsByQuizRef(string quizRef);






    }
}