using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet("games")]
        public IActionResult GetUserGames()
        {
            // Return games related to userId (Use identity to fetch ID)

            return Ok();
        }
        [HttpGet("mygames")]
        public IActionResult GetCreatedGames(int id)
        {
            // Return quizes created by user (Use identity to fetch ID)

            return Ok();
        }
    }
}
