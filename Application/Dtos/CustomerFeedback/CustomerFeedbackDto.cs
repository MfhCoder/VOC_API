using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CustomerFeedback
{
    public class CustomerFeedbackDto
    {
        public string MID { get; set; }
        public string MerchantName { get; set; }
        public string SurveyName { get; set; }
        public string Sentiment { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int BatchId { get; set; }
    }
}
