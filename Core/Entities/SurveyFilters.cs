using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("SurveyFilters")]
    public class SurveyFilters : BaseEntity
    {
        public string? Search { get; set; }
        public string? Type { get; set; }
        public string? Industry { get; set; }
        public string? License { get; set; }
        public string? Location { get; set; }
        public int? MinTenureInDays { get; set; }
        public int? MaxTenureInDays { get; set; }
        public string? Products { get; set; } // Store as comma-separated string
        public string? Ledgers { get; set; }  // Store as comma-separated string


        [Required]
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }
    }
}
