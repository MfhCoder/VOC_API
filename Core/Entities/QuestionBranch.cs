using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("QuestionBranches")]
    public class QuestionBranch : BaseEntity
    {
        [Required]
        [ForeignKey("ParentQuestion")]
        public int ParentQuestionId { get; set; }

        public virtual SurveyQuestion ParentQuestion { get; set; }

        [ForeignKey("TriggerOption")]
        public int? TriggerOptionId { get; set; }

        public virtual QuestionOption TriggerOption { get; set; }
    }
}