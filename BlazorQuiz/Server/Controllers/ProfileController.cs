using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlazorQuiz.Server.Data;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DbHelper _dbHelper;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
            _dbHelper = new DbHelper(context); ;
        }

        [HttpGet("mygames")]
        public IActionResult GetUserGames()
        {
            // Return games related to userId (Use identity to fetch ID)

            //Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            var userGames = _context.UserQuizModels.Where(q => q.UserRefId == userId).ToList();


            return Ok(userGames);
        }
        [HttpGet("myquizzes")]
        public IActionResult GetCreatedGames(int id)
        {
            // Return quizes created by user (Use identity to fetch ID)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            var userQuizzes = _context.Quizzes.Where(q => q.UserRefId == userId).ToList();

            return Ok(userQuizzes);
        }
    }
}
