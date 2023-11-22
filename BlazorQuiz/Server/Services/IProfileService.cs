using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public interface IProfileService
    {
        Task<List<UserCreatedQuizViewModel>> GetUserGamesAsync(string userId);
        Task<List<QuizModel>> GetUserCreatedGamesAsync(string userId);
        Task<List<UserQuizViewModel>> GetDataOnGameAsync(string publicId, string username);

    }
}
