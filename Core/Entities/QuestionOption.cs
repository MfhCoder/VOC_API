using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("QuestionOptions")]
    public class QuestionOption : BaseEntity
    {
        [Required]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual SurveyQuestion Question { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionText { get; set; }

        [Required]
        public bool IsTerminate { get; set; } = false;

        [Required]
        public bool TriggersBranch { get; set; } = false;

        public virtual ICollection<QuestionBranch> TriggeredBranches { get; set; }
        public virtual ICollection<FeedbackAnswer> Answers { get; set; }
    }
}