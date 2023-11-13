using BlazorQuiz.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {

        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {

            _profileService = profileService;
        }

        [HttpGet("mygames")]
        public async Task<IActionResult> GetUserGames()
        {
            // Return games related to userId (Use identity to fetch ID)

            var userGames = await _profileService.GetUserGamesAsync(UserId);

            return Ok(userGames);
        }
        [HttpGet("myquizzes")]
        public async Task<IActionResult> GetCreatedGames()
        {
            // Return quizes created by user (Use identity to fetch ID)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            var userQuizzes = await _profileService.GetUserCreatedGamesAsync(UserId);

            // Returns UserCreatedQuizViewModel List
            return Ok(userQuizzes);
        }
        [HttpGet("myquizzes/{id}")]
        public async Task<IActionResult> GetDataOnSpecificGame(string publicID)
        {
            // Return quizes created by user (Use identity to fetch ID)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            // Send data to client on all users who played this specific Quiz
            // Who played and what Score they got.

            var quizUserData = await _profileService.GetDataOnGameAsync(publicID);

            // Returns UserQuizViewModel List with score and user
            return Ok(quizUserData);
        }
    }
}
