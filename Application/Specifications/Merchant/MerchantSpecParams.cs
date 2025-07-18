using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Application.Specifications;

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

    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }

    public string? Type { get; set; }
    public string? Industry { get; set; }
    public string? Location { get; set; }
    public string? MinTenure { get; set; }
    public string? MaxTenure { get; set; }

    public int? MinTenureInDays { get; set; }
    public int? MaxTenureInDays { get; set; }

    private DateTime? _lastSurvey;
    public DateTime? LastSurvey
    {
        get => _lastSurvey;
        set => _lastSurvey = value.HasValue
            ? DateTime.SpecifyKind(value.Value.Date, DateTimeKind.Utc)
            : null;
    }

    private DateTime? _lastTicket;
    public DateTime? LastTicket
    {
        get => _lastTicket;
        set => _lastTicket = value.HasValue
            ? DateTime.SpecifyKind(value.Value.Date, DateTimeKind.Utc)
            : null;
    }

    private DateTime? _lastTransaction;
    public DateTime? LastTransaction
    {
        get => _lastTransaction;
        set => _lastTransaction = value.HasValue
            ? DateTime.SpecifyKind(value.Value.Date, DateTimeKind.Utc)
            : null;
    }

    private DateTime? _lastFeedback;
    public DateTime? LastFeedback
    {
        get => _lastFeedback;
        set => _lastFeedback = value.HasValue
            ? DateTime.SpecifyKind(value.Value.Date, DateTimeKind.Utc)
            : null;
    }
}
