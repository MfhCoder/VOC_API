using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class User : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Mobile { get; set; }

    [Required]
    public DateTime JoiningDate { get; set; } = DateTime.Now;

    [Required]
    public UserStatus Status { get; set; } = UserStatus.Active;

    [Required]
    [ForeignKey("Role")]
    public int RoleId { get; set; }

    public virtual Role Role { get; set; }

    public ICollection<Survey> SurveysCreated { get; set; }
    public ICollection<SurveyBatch> SurveyBatchesCreated { get; set; }
    public ICollection<Feedback> FeedbacksSubmitted { get; set; }
}

public enum UserStatus
{
    Active,
    Inactive,
    Pending,
    Deactivated
}