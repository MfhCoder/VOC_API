namespace Core.Entities;

// Feedback.cs
public class Feedback : BaseEntity
{
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }
    public int DeliveryId { get; set; }
    public SurveyDelivery Delivery { get; set; }
    public int MerchantId { get; set; }
    public Merchant Merchant { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string SubmittedBy { get; set; } 
    public AppUser Submitter { get; set; }
    public ICollection<FeedbackAnswer> Answers { get; set; }
    public ICollection<FeedbackTag> FeedbackTags { get; set; }
}
