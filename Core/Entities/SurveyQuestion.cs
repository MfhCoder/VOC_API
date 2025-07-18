using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("SurveyQuestions")]
    public class SurveyQuestion : BaseEntity
    {
        [Required]
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        [ForeignKey("Section")]
        public int? SectionId { get; set; }

        public virtual QuestionSection Section { get; set; }

        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int QuestionOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        [ForeignKey("QuestionType")]
        public int QuestionTypeId { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public virtual ICollection<QuestionOption> Options { get; set; }
        public virtual ICollection<QuestionBranch> ChildBranches { get; set; }
        public virtual ICollection<FeedbackAnswer> Answers { get; set; }
    }
}
