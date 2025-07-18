using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    [Table("SurveyBatches")]
    public class SurveyBatch : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        [Required]
        [ForeignKey("Channel")]
        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        [Required]
        public DateTime ScheduledTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MerchantCount { get; set; }

        [Required]
        [ForeignKey("Creator")]
        public int CreatedBy { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<SurveyDelivery> SurveyDelivery { get; set; }
    }
}
