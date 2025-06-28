using System.Diagnostics.Metrics;

namespace Core.Specifications;

public class MerchantSpecParams : PagingParams
{
    private List<string> _products = [];
    public List<string> Products
    {
        get => _products;
        set
        {
            _products = value.SelectMany(x => x.Split(',',
                StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    private List<string> _ladgers = [];
    public List<string> Ladger
    {
        get => _ladgers;
        set
        {
            _ladgers = value.SelectMany(x => x.Split(',',
                StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    public string? Sort { get; set; }

    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }

    public string? Type { get; set; }
    public string? Industry { get; set; }
    public string? Location { get; set; }

    //private string? _type;
    //public string Type
    //{
    //    get => _type ?? "";
    //    set => _type;
    //}

    //private string? _industry;
    //public string Industry
    //{
    //    get => _industry ?? "";
    //    set => _industry = value.ToLower();
    //}

    //private string? _location;
    //public string Location
    //{
    //    get => _location ?? "";
    //    set => _location = value.ToLower();
    //}

    public string? MinTenure { get; set; }
    public string? MaxTenure { get; set; }

    public int? MinTenureInDays { get; set; }
    public int? MaxTenureInDays { get; set; }
}
