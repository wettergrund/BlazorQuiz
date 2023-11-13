using BlazorQuiz.Server.Models;

namespace BlazorQuiz.Server.Services
{
    public interface IProfileService
    {
        Task<List<UserQuizModel>> GetUserGamesAsync(string userId);
        Task<List<QuizModel>> GetUserCreatedGamesAsync(string userId);
        Task<List<UserQuizModel>> GetDataOnGameAsync(string publicId);

    }
}
