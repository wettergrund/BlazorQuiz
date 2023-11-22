using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorQuiz.Shared.ViewModels
{
    public class SubmitQuizViewModel
    {
    

        public int gameId { get; set; }
        public List<GuessCheckViewModel> guesses { get; set; }
    }
}
