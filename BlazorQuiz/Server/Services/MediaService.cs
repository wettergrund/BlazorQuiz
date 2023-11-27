using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;

namespace BlazorQuiz.Server.Services
{
    public class MediaService : IMediaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        private readonly BlobServiceClient _blobStorageClient;

        public MediaService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
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

            Directory.CreateDirectory(destDir);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), destDir, fileName);
            // Save the file

            string blobConnectionString = _config["BlobConnectionString"];

            BlobServiceClient blob = new BlobServiceClient(blobConnectionString);
            var containerClient = blob.GetBlobContainerClient("blob");
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                var response = await blobClient.UploadAsync(stream, new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType  }
           
                });

            }


            var returnPath = fileName;



            return returnPath;
        }
    }
}
