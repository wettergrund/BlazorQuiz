namespace BlazorQuiz.Shared.ViewModels
{
    public class GameQuizViewModel
    {
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string? QuizMediaUrl { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
