using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.SurveyBuilder
{
    public class SurveyFiltersUpdateSpecification : BaseSpecification<SurveyFilters>
    {
        public SurveyFiltersUpdateSpecification(int surveyId)
            : base(s => s.SurveyId == surveyId)
        {
        }
    }
}
