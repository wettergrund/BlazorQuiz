using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MediaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{guid}")]
        public IActionResult GetMedia(Guid guid)
        {

            var media = _context.MediaModels.Where(file => file.Guid == guid).Single();
            
            return Ok(media);
        }

        [HttpPost]
        public async Task<IActionResult> UploadMediaAsync([FromForm]MediaModel media,IFormFile file)
        {
      
            //Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            media.Guid = Guid.NewGuid();

            var fileName = $"{media.Guid}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Update media model
            media.Path = filePath; 
            media.UserRefId = userId;
            media.Description = "Description";

            _context.Add(media);
            _context.SaveChanges();

            return Ok(new { media.Guid, filePath });
        }


    }
}
