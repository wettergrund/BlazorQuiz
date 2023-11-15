namespace BlazorQuiz.Client.Models
{
    public class QuizOptions
    {
        public double Timer { get; set; } = 60;
        public string QuizTitle { get; set; }
        public bool HasTimer { get; set; }
        public bool MediaQuestion { get; set; }
        public bool IsValidated { get; set; }
        public bool HasQuestion { get; set; }
      

        public void ResetToDefault()
        {
            MediaQuestion = false;
        }
    }
}
