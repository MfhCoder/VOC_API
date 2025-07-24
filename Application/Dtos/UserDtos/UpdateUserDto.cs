using Core.Entities;

namespace Application.Dtos.UserDtos;

public record UpdateUserDto(
    int? RoleId);

//public record UserFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Role { get; set; }
//    public string Status { get; set; }
//    public DateTime? StartDate { get; set; }
//    public DateTime? EndDate { get; set; }
//}