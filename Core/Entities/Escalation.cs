namespace Core.Entities;

public class Escalation :BaseEntity
{
    public int FeedbackId { get; set; }
    public Feedback Feedback { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public string Type { get; set; }
    public string Comment { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
}
