using BlazorQuiz.Server.Services;
using BlazorQuiz.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
   
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
          
            _gameService = gameService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserQuiz(int id)
        {
            // Get a specific game

            //Get a game
            var quizViewModel = await _gameService.GetUserQuizAsync(id);

            return Ok(quizViewModel);
        }
        [HttpPost("create/{title}")]
        public async Task<IActionResult> PostQuiz(string title, [FromBody] List<NewQuestionViewModel> questions, int seconds = 0)
        {

            //Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            if (userId == null)
            {
                return BadRequest();
            }

            //Created a new quiz
            var newQuiz = await _gameService.CreateQuizAsync(title, questions, seconds, userId);

            return Ok(newQuiz);
        }

        [HttpPost("create/{gameId}")]
        public async Task<IActionResult> PostNewGame(int gameId)
        {

            //Create a new game.
            //Use Identity to bind to creator user


            //Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            var newGame = await _gameService.CreateNewGameAsync(gameId, userId);

            return Ok(newGame);
        }

        [HttpPut]
        public IActionResult UpdateGame (int gameId, string gameState)
        {
            // Update game state + return guess result.
            return Ok();
        }


    }
}
