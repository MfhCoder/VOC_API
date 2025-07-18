using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("SurveyFilters")]
    public class SurveyFilters : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string ColumnName { get; set; }

        [Required]
        [StringLength(500)]
        public string ColumnValue { get; set; }

        [Required]
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }
    }
}
