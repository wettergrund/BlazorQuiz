using BlazorQuiz.Server.Services;
using BlazorQuiz.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
   
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
          
            _gameService = gameService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserQuiz(string id)
        {
            // Get a specific game

            //Get a game
            var quizViewModel = await _gameService.GetQuizByPublicIdAsync(id);

            return Ok(quizViewModel);
        }
        [HttpPost("create")]
        public async Task<IActionResult> PostQuiz([FromBody] NewQuizViewModel quiz)
        {

            //Created a new quiz
            var newQuiz = await _gameService.CreateQuizAsync(quiz.Title, quiz.Questions, quiz.Timer, UserId);

            return Ok(newQuiz);
        }

        [HttpPost("newgame/{quizId}")]
        public async Task<IActionResult> PostNewGame(string quizId)
        {

            //Create a new game.
            //Use Identity to bind to creator user

            var newGame = await _gameService.CreateNewGameAsync(quizId, UserId);

            return Ok(newGame);
        }
        [HttpPost("guess")]
        public async Task<IActionResult> GuessCheck([FromBody] GuessCheckViewModel guess)
        {
            var isCorrect = await _gameService.CheckGuess(guess);

            return Ok(isCorrect);
        }

        [HttpPut]
        public IActionResult UpdateGame (int gameId, string gameState)
        {
            // Update game state + return guess result.
            return Ok();
        }


    }
}
