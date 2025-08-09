using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Surveys")]
    public class Survey : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Type { get; set; }
        public bool? Connected_DB { get; set; }
        public bool? Status { get; set; }

        [StringLength(500)]
        public string? MessageContent { get; set; }
        public string? VisibleMerchantInfo{ get; set; }

        [Required]
        [ForeignKey("Creator")]
        public int CreatedBy { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<QuestionSection> QuestionSections { get; set; }
        public virtual ICollection<SurveyQuestion> Questions { get; set; }
        public virtual ICollection<SurveyBatch> SurveyBatches { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<SurveyFilters> SurveyFilters { get; set; }
        public virtual ICollection<SurveyDelivery> SurveyDelivery { get; set; }

    }
}