using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("FeedbackTags")]
    public class FeedbackTag
    {
        [Required]
        [ForeignKey("Feedback")]
        public int FeedbackId { get; set; }

        public virtual Feedback Feedback { get; set; }

        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}