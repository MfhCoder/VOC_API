using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("QuestionSections")]
    public class QuestionSection : BaseEntity
    {
        [Required]
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SectionOrder { get; set; }

        public virtual ICollection<SurveyQuestion> Questions { get; set; }
    }
}
