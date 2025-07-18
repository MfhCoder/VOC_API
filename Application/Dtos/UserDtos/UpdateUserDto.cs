using Core.Entities;

namespace Application.Dtos.UserDtos;

public record UpdateUserDto(
    string Name,
    string Mobile,
    int? RoleId,
    UserStatus Status);

//public record UserFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Role { get; set; }
//    public string Status { get; set; }
//    public DateTime? StartDate { get; set; }
//    public DateTime? EndDate { get; set; }
//}