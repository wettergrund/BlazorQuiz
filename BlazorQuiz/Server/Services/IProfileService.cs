using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public interface IProfileService
    {
        // [HttpGet("mygames")]
        Task<List<UserCreatedQuizViewModel>> GetUserGamesAsync(string userId);
        // [HttpGet("myquizzes")]
        Task<List<QuizModel>> GetUserCreatedGamesAsync(string userId);
        // [HttpGet("myquizzes/{publicid}")]
        Task<List<UserQuizViewModel>> GetDataOnGameAsync(string publicId, string username);

    }
}
