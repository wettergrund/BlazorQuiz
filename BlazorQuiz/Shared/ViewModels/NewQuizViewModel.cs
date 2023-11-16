namespace BlazorQuiz.Shared.ViewModels
{
    public class NewQuizViewModel
    {
        public string Title { get; set; }
        public string? PublicId { get; set; }
        public int Timer { get; set; }
        public List<NewQuestionViewModel> Questions { get; set; }
    }
}
