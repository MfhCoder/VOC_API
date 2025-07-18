using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("FeedbackAnswers")]
    public class FeedbackAnswer : BaseEntity
    {
        [Required]
        [ForeignKey("Feedback")]
        public int FeedbackId { get; set; }

        public virtual Feedback Feedback { get; set; }

        [Required]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual SurveyQuestion Question { get; set; }

        [ForeignKey("Option")]
        public int? OptionId { get; set; }

        public virtual QuestionOption Option { get; set; }

        [Range(-1.0, 1.0)]
        public float? SentimentScore { get; set; }

        [StringLength(4000)]
        public string? ResponseText { get; set; }
    }
}