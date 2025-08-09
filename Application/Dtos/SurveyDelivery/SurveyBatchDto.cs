namespace Application.Dtos.SurveyDelivery;

public class SurveyBatchDto
{
    public int BatchId { get; set; }
    public string Name { get; set; }
    public int SurveyId { get; set; }
    public string SurveyName { get; set; }
    public int ChannelId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public string ChannelName { get; set; }
    public int MerchantCount { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int SurveysSent { get; set; }
    public int SurveysDelivered { get; set; }
    public decimal DeliveryRate { get; set; } // Percentage
    public int SurveysResponses { get; set; }
    public decimal ResponseRate { get; set; } // Percentage
}