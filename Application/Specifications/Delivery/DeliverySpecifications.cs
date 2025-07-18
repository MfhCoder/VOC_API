using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Delivery
{
    public class DeliverySpecifications : BaseSpecification<SurveyBatch>
    {
        public DeliverySpecifications(DeliverySpecParams specParams)
            : base(x =>
                (specParams.BatchId == null || x.Id == specParams.BatchId) &&
                (string.IsNullOrEmpty(specParams.SurveyType) || x.Survey.Name.ToString() == specParams.SurveyType) &&
                (string.IsNullOrEmpty(specParams.Channel) || x.Channel.Name.ToString() == specParams.Channel) &&
                (string.IsNullOrEmpty(specParams.Status) || x.Status.ToString() == specParams.Status) &&
                (!specParams.StartDate.HasValue || x.CreatedAt >= specParams.StartDate) &&
                (!specParams.EndDate.HasValue || x.CreatedAt <= specParams.EndDate)
            )
        {
            //AddInclude(x => x.Attempts);
            //AddInclude(x => x.Recipients);
            AddInclude(x => x.Survey);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "dateAsc":
                        AddOrderBy(x => x.CreatedAt);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(x => x.CreatedAt);
                        break;
                    default:
                        AddOrderByDescending(x => x.CreatedAt);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(x => x.CreatedAt);
            }
        }
    }

}
