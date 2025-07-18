using Core.Entities;

namespace Application.Specifications;

public class LocationListSpecification : BaseSpecification<Merchant, string>
{
    public LocationListSpecification()
    {
        AddSelect(x => x.Location);
        ApplyDistinct();
    }
}
