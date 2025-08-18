using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.SurveyDelivery
{
    public class SurveyBatchCreateDto
    {
        public int SurveyId { get; set; }
        public int ChannelId { get; set; }
        public DateTime? ScheduledTime { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Merchants list must contain at least one merchant.")]
        public required List<int> Merchants { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Merchants != null && Merchants.Contains(0))
            {
                yield return new ValidationResult("Merchants list cannot include 0.", new[] { nameof(Merchants) });
            }
        }
    }
}
