namespace BlazorQuiz.Shared.ViewModels
{
    public class NewQuestionViewModel
    {
        public NewQuestionViewModel()
        {
            
        }
        public NewQuestionViewModel(NewQuestionViewModel clone)
        {
            Question = clone.Question;
            Answer1 = clone.Answer1;
            Answer2 = clone.Answer2;
            Answer3 = clone.Answer3;
            Answer4 = clone.Answer4;
            CorrectAnswer = clone.Answer1;
           
        }
        public string QuizImageUrl { get; set; } = "";
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string CorrectAnswer { get; set; }

        public void ResetToDefault()
        {
            Question = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Answer4 = string.Empty;
            QuizImageUrl = string.Empty;
        }

    }
}
