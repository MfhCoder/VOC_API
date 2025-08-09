using Application.Dtos.SurveyDelivery;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.SurveyDelivery
{
    public class SurveyBatchSpecification : BaseSpecification<SurveyBatch>
    {
        public SurveyBatchSpecification(SurveyBatchFilterParams filterParams)
            : base(x => x.SurveyId == filterParams.SurveyId)
        {
            // Apply sorting
            if (!string.IsNullOrEmpty(filterParams.Sort))
            {
                switch (filterParams.Sort.ToLower())
                {
                    case "CreatedAt":
                        AddOrderBy(s => s.CreatedAt);
                        break;
                    case "CreatedAtdesc":
                        AddOrderByDescending(s => s.CreatedAt);
                        break;
                    default:
                        AddOrderByDescending(s => s.Id);
                        break;
                }
            }

            // Apply pagination
            ApplyPaging((filterParams.PageIndex - 1) * filterParams.PageSize, filterParams.PageSize);

            AddInclude(s => s.Survey);
            AddInclude(s => s.Channel);
            AddInclude(s => s.SurveyDelivery);
            AddInclude("Survey.Feedbacks");
        }

        public SurveyBatchSpecification(SurveyLogFilterParams filter)
          : base(b =>
              (!filter.From.HasValue || b.CreatedAt >= filter.From.Value) &&
              (!filter.To.HasValue || b.CreatedAt <= filter.To.Value) &&
              (filter.SurveyTypes.Count == 0 || filter.SurveyTypes.Contains(b.Survey.Name)) &&
              (filter.Channels.Count == 0 || filter.Channels.Contains(b.Channel.Name)) &&
              //(!filter.ScoreMin.HasValue || b.AverageScore >= filter.ScoreMin.Value) &&//???
              //(!filter.ScoreMax.HasValue || b.AverageScore <= filter.ScoreMax.Value) &&//???
              (!filter.DeliveryRateMin.HasValue ||
                  (b.SurveyDelivery.Count() > 0 &&
                   ((decimal)(b.Survey.SurveyDelivery != null ? b.Survey.SurveyDelivery.Where(s => s.Status == "Delivered").Count() : 0) / b.SurveyDelivery.Count()) * 100 >= filter.DeliveryRateMin.Value)) &&
              (!filter.DeliveryRateMax.HasValue ||
                  (b.SurveyDelivery.Count() > 0 &&
                   ((decimal)(b.Survey.SurveyDelivery != null ? b.Survey.SurveyDelivery.Where(s => s.Status == "Delivered").Count() : 0) / b.SurveyDelivery.Count()) * 100 <= filter.DeliveryRateMax.Value)) &&
              (!filter.ResponseRateMin.HasValue ||
                  (b.SurveyDelivery.Count() > 0 &&
                   ((decimal)(b.Survey.Feedbacks != null ? b.Survey.Feedbacks.Count() : 0) / b.SurveyDelivery.Count()) * 100 >= filter.ResponseRateMin.Value)) &&
              (!filter.ResponseRateMax.HasValue ||
                  (b.SurveyDelivery.Count() > 0 &&
                   ((decimal)(b.Survey.Feedbacks != null ? b.Survey.Feedbacks.Count() : 0) / b.SurveyDelivery.Count()) * 100 <= filter.ResponseRateMax.Value)) &&
              (filter.BatchId == null || b.Id == filter.BatchId)
          )
        {
            // Apply sorting
            if (!string.IsNullOrEmpty(filter.Sort))
            {
                switch (filter.Sort.ToLower())
                {
                    case "CreatedAt":
                        AddOrderBy(s => s.CreatedAt);
                        break;
                    case "CreatedAtdesc":
                        AddOrderByDescending(s => s.CreatedAt);
                        break;
                    default:
                        AddOrderByDescending(s => s.Id);
                        break;
                }
            }

            AddInclude(b => b.Channel);
            AddInclude(b => b.SurveyDelivery);
            AddInclude(b => b.Survey);
            AddInclude("Survey.Feedbacks");
        }

        public SurveyBatchSpecification()
        {
            AddInclude(b => b.Channel);
            AddInclude(b => b.SurveyDelivery);
            AddInclude(b => b.Survey);
            AddInclude("Survey.Feedbacks");
        }
    }
}
