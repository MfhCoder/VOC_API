namespace Core.Entities;

public class FeedbackTag 
{
    public int FeedbackId { get; set; }
    public Feedback Feedback { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
