using Application.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SurveyBuilder
{
    public class SurveyFilterParams: PagingParams
    {
        public string? Search { get; set; }
        public string? Sort { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
