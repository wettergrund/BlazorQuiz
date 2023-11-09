using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
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
      
            //Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            var newMedia = await _mediaService.UploadMediaAsync(media, file, userId);

            return Ok(newMedia);
        }


    }
}
