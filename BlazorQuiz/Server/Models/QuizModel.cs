using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorQuiz.Server.Models
{
    public class QuizModel
    {
        [Key]
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public int Timer { get; set; }

        [ForeignKey("User")]
        public string UserRefId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<QuestionModel> Questions { get; set; }
    }
}
