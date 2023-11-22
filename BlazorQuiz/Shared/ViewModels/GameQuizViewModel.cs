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
}
