using Application.Dtos.CustomerFeedback;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.CustomerFeedback
{
    public class CustomerFeedbackSpecification : BaseSpecification<Feedback>
    {
        public CustomerFeedbackSpecification(CustomerFeedbackFilterParams filter)
            : base(f =>
                (!filter.From.HasValue || f.SubmittedAt >= filter.From.Value) &&
                (!filter.To.HasValue || f.SubmittedAt <= filter.To.Value) &&
                (
                    string.IsNullOrEmpty(filter.Search) ||
                    (
                        filter.SearchColumn == "MID" && f.Merchant != null && f.MerchantId == int.Parse(filter.Search))
                    ) ||
                    (
                        filter.SearchColumn == "MerchantName" && f.Merchant != null && f.Merchant.Name.Contains(filter.Search)
                    ) ||
                    (
                        filter.SearchColumn == "SurveyName" && f.Survey.Name.Contains(filter.Search)
                    ) 
            )
        {
            AddInclude(f => f.Merchant);
            AddInclude(f => f.Survey);
            AddInclude(f => f.FeedbackTags);
            AddInclude(f => f.Delivery);
        }

        public CustomerFeedbackSpecification(int id)
           : base(x => x.Id == id)
        {
            AddInclude(f => f.Merchant);
            AddInclude(f => f.Delivery);
            AddInclude(f => f.Survey);
            AddInclude(f => f.FeedbackTags);
        }
    }
}
