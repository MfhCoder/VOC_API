using Core.Entities;

namespace Application.Specifications;

public class IndustryListSpecification : BaseSpecification<Merchant, string>
{
    public IndustryListSpecification()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
