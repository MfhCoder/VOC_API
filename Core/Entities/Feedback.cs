using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Feedbacks")]
    public class Feedback : BaseEntity
    {
        [Required]
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        [ForeignKey("Delivery")]
        public int? DeliveryId { get; set; }

        public virtual SurveyDelivery Delivery { get; set; }

        [ForeignKey("Merchant")]
        public int? MerchantId { get; set; }

        public virtual Merchant Merchant { get; set; }

        [Required]
        public DateTime SubmittedAt { get; set; }

        [ForeignKey("Submitter")]
        public int? SubmittedBy { get; set; }

        public virtual User Submitter { get; set; }

        public virtual ICollection<FeedbackAnswer> Answers { get; set; }
        public virtual ICollection<FeedbackTag> FeedbackTags { get; set; }
        public virtual ICollection<Escalation> Escalations { get; set; }
    }
}