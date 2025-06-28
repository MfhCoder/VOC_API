using Core.Entities;

namespace Core.Specifications;

public class LocationListSpecification : BaseSpecification<Merchant, string>
{
    public LocationListSpecification()
    {
        AddSelect(x => x.Location);
        ApplyDistinct();
    }
}
