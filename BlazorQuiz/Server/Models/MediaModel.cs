using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorQuiz.Server.Models
{
    public class MediaModel
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        [ForeignKey("User")]
        public string UserRefId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
