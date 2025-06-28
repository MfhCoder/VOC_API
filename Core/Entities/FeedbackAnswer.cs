namespace Core.Entities;

public class FeedbackAnswer : BaseEntity
{
    public int FeedbackId { get; set; }
    public Feedback Feedback { get; set; }
    public int QuestionId { get; set; }
    public SurveyQuestion Question { get; set; }
    public float? SentimentScore { get; set; }
    public string ResponseText { get; set; }
}
