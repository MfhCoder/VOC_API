using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("SurveyDeliveries")]
    public class SurveyDelivery : BaseEntity
    {
        [Required]
        [ForeignKey("Batch")]
        public int BatchId { get; set; }

        public virtual SurveyBatch Batch { get; set; }

        [Required]
        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }

        public virtual Merchant Merchant { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public int? RetryCount { get; set; }
        [ForeignKey("SurveyId")]
        public int? SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        [ForeignKey("Link")]
        public int? LinkId { get; set; }

        public virtual DeliveryLink Link { get; set; }

        [StringLength(500)]
        public string? EncryptionToken { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }

}
