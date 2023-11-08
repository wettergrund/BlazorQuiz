using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DbHelper _dbHelper;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
            _dbHelper = new DbHelper(context); ;
        }
        [HttpGet("{id}")]
        public IActionResult GetUserQuiz(int id)
        {
            // Get a specific game

            //Get a game
            var userquiz = _context.UserQuizModels.Where(game => game.Id == id).FirstOrDefault();

            var quizId = userquiz.QuizRefId;

            //Find the quiz
            var game = _context.Quizzes.Where(q => q.Id == quizId).FirstOrDefault();

            return Ok(new { userquiz , game });
        }
        [HttpPost("create/{title}")]
        public IActionResult PostQuiz(string title, [FromBody] List<NewQuestionViewModel> questions, int seconds = 0)
        {

            //Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            //Created a new quiz
            QuizModel newQuiz = new QuizModel() { 
                Name = title,
                PublicId = Guid.NewGuid(),
                Timer = seconds,
                UserRefId = userId
                

            };

            _context.Add(newQuiz);
            _context.SaveChanges();

            //Create new questions and bind to quiz.
            
            foreach (var question in questions)
            {
                QuestionModel newQuestion = new QuestionModel(question)
                {
                    QuizRefId = newQuiz.Id,
                    MediaRefId = 0 //0 for now

                };

                _context.Add(newQuestion);              
                
            }

            _context.SaveChanges();

            return Ok(newQuiz);
        }

        [HttpPost("create/{gameId}")]
        public IActionResult PostNewGame(int gameId)
        {

            //Create a new game.
            //Use Identity to bind to creator user

            var quiz = _dbHelper.FindQuiz(gameId); //Finding game based on ID


            //Get user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            UserQuizModel newGame = new UserQuizModel() { 
                QuizRefId=quiz.Id,
                UserRefId=userId,
                Score = 0,
            };
            _context.Add(newGame);
            _context.SaveChanges();



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
