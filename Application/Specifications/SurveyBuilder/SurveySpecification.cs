using Application.Dtos.SurveyBuilder;
using Core.Entities;

namespace Application.Specifications.SurveyBuilder
{
    public class SurveySpecification : BaseSpecification<Survey>
    {
        public SurveySpecification(SurveyFilterParams filterParams)
           : base(x =>
               (string.IsNullOrEmpty(filterParams.Search) || x.Name.ToLower().Contains(filterParams.Search.ToLower())) &&
               (!filterParams.StartDate.HasValue || x.CreatedAt >= filterParams.StartDate.Value) &&
               (!filterParams.EndDate.HasValue || x.CreatedAt <= filterParams.EndDate.Value)
           )
        {
            AddInclude(x => x.Feedbacks);

                ApplyPaging(filterParams.PageSize * (filterParams.PageIndex - 1), filterParams.PageSize);

                if (!string.IsNullOrEmpty(filterParams.Sort))
                {
                    switch (filterParams.Sort)
                    {
                        case "NameAsc": AddOrderBy(u => u.Name); break;
                        case "NameDesc": AddOrderByDescending(u => u.Name); break;
                        case "CreatedDateAsc": AddOrderBy(u => u.CreatedAt); break;
                        case "CreatedDateDesc": AddOrderByDescending(u => u.CreatedAt); break;
                        default: AddOrderBy(u => u.Id); break;
                    }
                }


            // Apply pagination
            ApplyPaging((filterParams.PageIndex - 1) * filterParams.PageSize, filterParams.PageSize);
        }

        public SurveySpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Feedbacks);
        }
    }
}
