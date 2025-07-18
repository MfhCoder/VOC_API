using Application.Specifications;

namespace Application.Dtos.UserDtos;

public class UserFilterParams : PagingParams
{
    public string? Search { get; set; }
    public string? Role { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}