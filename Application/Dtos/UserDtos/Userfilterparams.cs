using Application.Specifications;
using Core.Entities;

namespace Application.Dtos.UserDtos;

public class UserFilterParams : PagingParams
{
    public string? Search { get; set; }
    public string? Role { get; set; }
    public UserStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}