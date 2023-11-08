using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        [HttpGet("{guid}")]
        public IActionResult GetMedia(Guid guid)
        {

            // Return media based on guid
            return Ok();
        }

        [HttpPost]
        public IActionResult UploadMedia()
        {

            // Save media to storage and add reference to DB
            return Ok();
        }


    }
}
