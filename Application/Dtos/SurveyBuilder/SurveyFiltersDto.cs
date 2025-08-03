namespace Application.Dtos.SurveyBuilder
{
    public class SurveyFiltersDto
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string? Type { get; set; }
        public string? Industry { get; set; }
        public string? License { get; set; }
        public string? Location { get; set; }
        public int? MinTenureInDays { get; set; }
        public int? MaxTenureInDays { get; set; }
        public List<string>? Products { get; set; }
        public List<string>? Ledgers { get; set; }
    }
}
