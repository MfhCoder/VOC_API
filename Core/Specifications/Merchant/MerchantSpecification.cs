using Core.Entities;

namespace Core.Specifications;

public class MerchantSpecification : BaseSpecification<Merchant>
{
    public MerchantSpecification(MerchantSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
        (string.IsNullOrEmpty(specParams.Type) || x.Type == specParams.Type) &&
        (string.IsNullOrEmpty(specParams.Industry) || x.Industry == specParams.Industry) &&
        (string.IsNullOrEmpty(specParams.Location) || x.Location == specParams.Location) &&
        (!specParams.MinTenureInDays.HasValue || x.TenureInDays >= specParams.MinTenureInDays.Value) &&
        (!specParams.MaxTenureInDays.HasValue || x.TenureInDays <= specParams.MaxTenureInDays.Value) &&
        (specParams.Products.Count == 0 || specParams.Products.Contains(x.Product)) &&
        (specParams.Ladger.Count == 0 || specParams.Ladger.Contains(x.Ledger))
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex -1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "LastSurveyAsc":
                AddOrderBy(x => x.LastSurvey);
                break;
            case "LastSurveyDesc":
                AddOrderByDescending(x => x.LastSurvey);
                break;
            case "LastFeedbackAsc":
                AddOrderBy(x => x.LastFeedback);
                break;
            case "LastFeedbackDesc":
                AddOrderByDescending(x => x.LastFeedback);
                break;
            case "LastTicketAsc":
                AddOrderBy(x => x.LastTicket);
                break;
            case "LastTicketDesc":
                AddOrderByDescending(x => x.LastTicket);
                break;
            case "TenureAsc":
                AddOrderBy(x => x.TenureInDays);
                break;
            case "TenureDesc":
                AddOrderByDescending(x => x.TenureInDays);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
