using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public class MediaService : IMediaService
    {
        private readonly ApplicationDbContext _context;
        public MediaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<MediaModel> GetMediaByIdAsync(Guid guid)
        {
            var media = _context.MediaModels.Where(file => file.Guid == guid).Single();

            return media;
        }

        public async Task<NewMediaViewModel> UploadMediaAsync(IFormFile file, string userId)
        {


            var mediaGuid = Guid.NewGuid();
            var fileName = $"{mediaGuid}{Path.GetExtension(file.FileName)}";
            var filePath = await SaveFileAsync(file, fileName); // This is a call to the new method

            var newMedia = new MediaModel
            {
                Guid = mediaGuid,
                Path = filePath,
                Type = file.ContentType,
                UserRefId = userId,
                Description = "Des" //Changed to some input from user?,
                

            };

            _context.Add(newMedia);
            await _context.SaveChangesAsync();

            var viewModel = new NewMediaViewModel
            {
                Guid = newMedia.Guid,
                Path = newMedia.Path
            };

            return viewModel;

        }
        private async Task<string> SaveFileAsync(IFormFile file, string fileName)
        {
            //Save file to disk and return relative path
            string imageDir = "wwwroot/images";
            string videoDir = "wwwroot/videos";
            string destDir = imageDir;


            if (file.ContentType.StartsWith("video"))
            {
                bool videoDirExist = Directory.Exists(videoDir);

                if (!videoDirExist)
                {
                    Directory.CreateDirectory(videoDir);
                }

                destDir = videoDir;

            }
            else if(file.ContentType.StartsWith("image"))
            {
                bool imageDirExist = Directory.Exists(imageDir);

                if (!imageDirExist)
                {
                    Directory.CreateDirectory(imageDir);
                }

            }

            Directory.CreateDirectory(fileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), destDir, fileName);
            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            destDir = destDir.Replace("wwwroot/", "");
            var returnPath = Path.Combine(destDir, fileName);

            returnPath = returnPath.Replace("\\", "/");


            return returnPath;
        }
    }
}
