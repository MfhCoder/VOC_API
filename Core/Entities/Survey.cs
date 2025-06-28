namespace Core.Entities;

public class Survey : BaseEntity
{
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public AppUser Creator { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<SurveyQuestion> Questions { get; set; }
}
