using BlazorQuiz.Shared.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlazorQuiz.Server.Models
{
    public class QuestionModel
    {

        public QuestionModel() { }
        public QuestionModel(NewQuestionViewModel input)
        {
            Question = input.Question;
            Answer1 = input.Answer1;
            Answer2 = input.Answer2;
            Answer3 = input.Answer3;
            Answer4 = input.Answer4;
        }

        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        [ForeignKey("Quiz")]
        public string QuizRefId { get; set; }
        [JsonIgnore]
        public virtual QuizModel Quiz { get; set; }

        [ForeignKey("Media")]
        public int? MediaRefId { get; set; }
        public virtual MediaModel Media { get; set; }
    }
}
