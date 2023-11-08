using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorQuiz.Server.Models
{
    public class UserQuizModel
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }

        [ForeignKey("Quiz")]
        public int QuizRefId { get; set; }
        public virtual QuizModel Quiz { get; set; }

        [ForeignKey("User")]
        public string UserRefId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
