using Core.Entities;
using Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class VocContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<QuestionType> QuestionTypes { get; set; }
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<SurveyBatch> SurveyBatches { get; set; }
    public DbSet<SurveyDelivery> SurveyDeliveries { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<FeedbackAnswer> FeedbackAnswers { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<FeedbackTag> FeedbackTags { get; set; }
    public DbSet<Escalation> Escalations { get; set; }
    public DbSet<DeliveryLink> DeliveryLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite key for FeedbackTag
        modelBuilder.Entity<FeedbackTag>()
            .HasKey(ft => new { ft.FeedbackId, ft.TagId });

        // Configure AppUser relationships
        modelBuilder.Entity<Survey>()
            .HasOne(s => s.Creator)
            .WithMany()
            .HasForeignKey(s => s.CreatedBy);

        modelBuilder.Entity<SurveyBatch>()
            .HasOne(sb => sb.Creator)
            .WithMany()
            .HasForeignKey(sb => sb.CreatedBy);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Submitter)
            .WithMany()
            .HasForeignKey(f => f.SubmittedBy);
    }
}

