using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorQuiz.Shared.ViewModels
{
    public class GameQuizViewModel
    {
        public string title { get; set; }
        public string? publicId { get; set; }
        public int timer { get; set; }
        public List<QuestionSharedViewModel> questions { get; set; }
        public int gameId { get; set; }
    }
    public class GuessCheckViewModel
    {
        public int GuessId { get; set; }
        public string Guess { get; set; }
        public int Seconds { get; set; }

    }

    public class UserQuizViewModel
    {
        public int Score { get; set; }
        public string User { get; set; }
    }

    public class SubmitQuizViewModel
    {
        public int gameId { get; set; }
        public List<GuessCheckViewModel> guesses { get; set; }
    }


}
