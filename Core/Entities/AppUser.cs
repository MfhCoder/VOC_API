using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public ICollection<Survey> SurveysCreated { get; set; }
    public ICollection<SurveyBatch> SurveyBatchesCreated { get; set; }
    public ICollection<Feedback> FeedbacksSubmitted { get; set; }
}
