using Application.Specifications;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CustomerFeedback
{
    public class CustomerFeedbackFilterParams : PagingParams
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Search { get; set; }
        [SwaggerParameter("Search column. Allowed values: 'MID', 'MerchantName', 'SurveyName'")]
        public string? SearchColumn { get; set; } // "MID", "MerchantName", "SurveyName"

    }
}
