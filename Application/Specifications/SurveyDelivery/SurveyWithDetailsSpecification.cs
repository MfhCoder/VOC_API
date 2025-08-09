using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.SurveyDelivery
{
    public class SurveyWithDetailsSpecification : BaseSpecification<Survey>
    {
        public SurveyWithDetailsSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Feedbacks);
            AddInclude(x => x.SurveyBatches);
            AddInclude(x => x.SurveyDelivery);
        }
    }
}
