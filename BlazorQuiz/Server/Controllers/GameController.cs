using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetGame(int id)
        {
            // Get a specific game
            return Ok();
        }
        [HttpPost("create/{title}")]
        public IActionResult PostQuiz(string title)
        {

            //Create a new quiz
            return Ok();
        }

        [HttpPost("create/{gameId}")]
        public IActionResult PostNewGame(int gameId)
        {

            //Create a new game.
            //Use Identity to bind to creator user

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateGame (int gameId, string gameState)
        {
            // Update game state + return guess result.
            return Ok();
        }






    }
}
