namespace BlazorQuiz.Server.Models.ViewModels
{
    public class QuizViewModel
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string[] Answer { get; set; }
    }
}
