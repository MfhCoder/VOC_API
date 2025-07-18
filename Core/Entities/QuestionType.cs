using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("QuestionTypes")]
    public class QuestionType : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<SurveyQuestion> Questions { get; set; }
    }
}
