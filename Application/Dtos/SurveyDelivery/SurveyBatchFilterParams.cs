using Application.Specifications;

namespace Application.Dtos.SurveyDelivery
{
    public class SurveyBatchFilterParams : PagingParams
    {
        public int SurveyId { get; set; }
    }
}
