using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorQuiz.Shared.ViewModels
{
    public class NewQuizViewModel
    {

        public string Title { get; set; }
        public int Timer { get; set; }
        public List<NewQuestionViewModel> Questions { get; set; }


    }
}
