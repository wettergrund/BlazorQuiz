using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;
using BlazorQuiz.Shared.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public interface IGameService
    {
        // [HttpGet("{id}")]
        Task<GameWithQuizViewModel> GetQuizByPublicIdAsync(string id);

        // [HttpPost("create")]
        Task<QuizModel> CreateQuizAsync(string title, List<NewQuestionViewModel> questions, int seconds, string userId);

        // [HttpPost("newgame/{quizId}")]
        Task<QuizViewModel> CreateNewGameAsync(string quizId, string userId);

        // [HttpPost("guess")]
        Task<bool> CheckGuess(GuessCheckViewModel guess);

        // [HttpPut("gameresult")]
        Task<bool> UpdateGameAsync(int gameId, string gameState);
        // [HttpPut("gameresult")]
        Task<UserQuizModel> FinishedGame(UserQuizModel quiz, List<GuessCheckViewModel> guesses);
        // [HttpPut("gameresult")]
        UserQuizModel FindUserQuiz(int id);
        // [HttpPut("gameresult")]
        List<QuestionModel> FindQuestionsByQuizRef(string quizRef);






    }
}