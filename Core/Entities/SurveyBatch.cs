namespace Core.Entities;

public class SurveyBatch : BaseEntity
{
    public string Name { get; set; }
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }
    public int ChannelId { get; set; }
    public Channel Channel { get; set; }
    public DateTime ScheduledTime { get; set; }
    public string Status { get; set; }
    public int MerchantCount { get; set; }
    public string CreatedBy { get; set; } 
    public AppUser Creator { get; set; }
    public DateTime CreatedAt { get; set; }
}
