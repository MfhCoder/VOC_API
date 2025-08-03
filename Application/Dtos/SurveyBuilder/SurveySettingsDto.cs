using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.SurveyBuilder
{
    public class SurveySettingsDto
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Type { get; set; }
        public bool? Connected_DB { get; set; }
        public bool? Status { get; set; }

        [StringLength(500)]
        public string? MessageContent { get; set; }

    }
}
