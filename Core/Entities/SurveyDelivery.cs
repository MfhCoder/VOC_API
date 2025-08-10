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

        // Represents the URL for the survey, which is a combination of the site URL from app settings and an encryption token.
        [Required]
        [StringLength(500)]
        public string SurveyURL { get; set; } // SiteURL(From appsetting) + EncryptionToken

        // Represents the expiration date of the survey link, which is required.
        // This date is typically set to five days from the current date.
        [Required]
        public DateTime LinkExpirationDate { get; set; } // five days from now

        // Represents an optional encryption token for the survey, which is a string that combines the MerchantId and SurveyId.
        [StringLength(500)]
        public string EncryptionToken { get; set; } // encrypted (MerchantId+"-"+SurveyId)

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }

}
