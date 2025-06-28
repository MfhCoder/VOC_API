using Core.Entities;

namespace Core.Specifications;

public class IndustryListSpecification : BaseSpecification<Merchant, string>
{
    public IndustryListSpecification()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
