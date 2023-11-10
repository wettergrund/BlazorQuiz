namespace BlazorQuiz.Shared.ViewModels
{
    public class NewQuestionViewModel
    {
        public string QuizImageUrl { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
