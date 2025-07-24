using Application.Specifications;

namespace Application.Dtos.RoleDtos;

public class RoleFilterParams : PagingParams
{
    public string? Search { get; set; }
}