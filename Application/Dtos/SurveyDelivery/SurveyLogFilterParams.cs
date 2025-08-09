using Application.Specifications;

namespace Application.Dtos.SurveyDelivery
{
    public class SurveyLogFilterParams : PagingParams
    {
        public int? BatchId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        // Survey Type: NPS, CSAT, CES, Other (multi-select)
        public List<string> SurveyTypes { get; set; } = new List<string>();
        // Survey Channel: SMS, Whatsapp, Email, In-app (multi-select)
        public List<string> Channels { get; set; } = new List<string>();

        // Survey Score
        public decimal? ScoreMin { get; set; }
        public decimal? ScoreMax { get; set; }

        // Delivery Rate (percent)
        public decimal? DeliveryRateMin { get; set; }
        public decimal? DeliveryRateMax { get; set; }

        // Response Rate (percent)
        public decimal? ResponseRateMin { get; set; }
        public decimal? ResponseRateMax { get; set; }
    }
}
