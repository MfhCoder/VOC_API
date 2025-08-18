using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CustomerFeedback
{
    public class FeedbackDetailsDto
    {
        public string SurveyName { get; set; }
        public bool? Sentiment { get; set; }
        public string MerchantId { get; set; }
        public string? MerchantName { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int? BatchId { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; }
        public List<string> FeedbackTags { get; set; }
    }

    public class QuestionAnswerDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
