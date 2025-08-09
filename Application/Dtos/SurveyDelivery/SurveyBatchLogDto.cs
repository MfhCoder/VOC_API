using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SurveyDelivery
{
    public class SurveyBatchLogDto
    {
        public string BatchId { get; set; }
        public string Type { get; set; }
        public string ChannelName { get; set; }
        public decimal AverageScore { get; set; }
        public int SurveysSent { get; set; }
        public int SurveysDelivered { get; set; }
        public decimal DeliveryRate { get; set; }
        public int SurveysResponses { get; set; }
        public decimal ResponseRate { get; set; }
    }
}
