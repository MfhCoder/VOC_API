namespace Application.Dtos.SurveyDelivery
{
    public class SurveyStatisticsDto
    {
        public int SurveysSent { get; set; }
        public int SurveysDelivered { get; set; }
        public int SurveysResponses { get; set; }
        public decimal ResponseRate { get; set; }
    }
}
