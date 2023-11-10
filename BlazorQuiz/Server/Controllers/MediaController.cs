using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : BaseController
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService )
        {
            _mediaService = mediaService;
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetMedia(Guid guid)
        {

            var media = await _mediaService.GetMediaByIdAsync(guid);

            return Ok(media);
        }

        [HttpPost]
        public async Task<IActionResult> UploadMediaAsync([FromForm]MediaModel media,IFormFile file)
        {
      
          

            var newMedia = await _mediaService.UploadMediaAsync(media, file, UserId);

            return Ok(newMedia);
        }


    }
}
