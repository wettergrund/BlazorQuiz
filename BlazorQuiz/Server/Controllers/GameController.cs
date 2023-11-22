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

        [HttpPut("gameresult")]
        public async Task<IActionResult> UpdateGame ([FromBody] SubmitQuizViewModel quiz)
        {


            /* 1. Get UserGuiz(Game) by gameID 
             * 2. Get all questions with QuizRefId == UserGuiz.QuizRefPublicId
             * 3. Verify number of guesses == questions.
             * 4. Foreach guess, verify guesses and update score for gameId
             */


            //Find UserQuiz and questions related to Quiz Id
            var userGame = _gameService.FindUserQuiz(quiz.gameId);
            var questions = _gameService.FindQuestionsByQuizRef(userGame.QuizRefPublicId);

            int userGuesses = quiz.guesses.Count();
            int quizGuesses = questions.Count();


            //Make sure sent in questions are equal to questions related to quiz
            if (!userGuesses.Equals(quizGuesses)) {

                return BadRequest("Number of guesses does not match");

            }
            else if (userGuesses.Equals(quizGuesses)) {

                //Update score and return game info.
                var updateScore = await _gameService.FinishedGame(userGame , quiz.guesses);

                return Ok(updateScore.Score);
            
            }



            return BadRequest("Problem to update game");
        }


    }
}
