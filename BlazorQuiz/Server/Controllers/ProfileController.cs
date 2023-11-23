using BlazorQuiz.Server.Services;
using Microsoft.AspNetCore.Mvc;

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
            var userGames = await _profileService.GetUserGamesAsync(UserId);

            // Return games related to userId (Use identity to fetch ID)
            return Ok(userGames);
        }

        // Return quizes created by user (Use identity to fetch ID)
        //Get user ID from header
        [HttpGet("myquizzes")]
        public async Task<IActionResult> GetCreatedGames()
        {
            var userQuizzes = await _profileService.GetUserCreatedGamesAsync(UserId);

            // Returns UserCreatedQuizViewModel List
            return Ok(userQuizzes);
        }

        // Send data to client on all users who played this specific Quiz
        // Who played and what Score they got.
        [HttpGet("myquizzes/{publicid}")]
        public async Task<IActionResult> GetDataOnSpecificGame(string publicId)
        {
            var quizUserData = await _profileService.GetDataOnGameAsync(publicId, UserName);

            // Returns UserQuizViewModel List with score and username
            return Ok(quizUserData);
        }
    }
}
