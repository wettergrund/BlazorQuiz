using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public interface IMediaService
    {

        Task<MediaModel> GetMediaByIdAsync(Guid guid);

        Task<NewMediaViewModel> UploadMediaAsync(MediaModel media, IFormFile file, string userId);
    }
}
