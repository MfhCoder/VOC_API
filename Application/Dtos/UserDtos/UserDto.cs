using Core.Entities;

namespace Application.Dtos.UserDtos;

public record UserDto(
    int Id,
    string Name,
    string Email,
    string Mobile,
    DateTime JoiningDate,
    UserStatus Status,
    string RoleName);

//public class UserDto
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//    public string Mobile { get; set; }
//    public DateTime JoiningDate { get; set; }
//    public string UserStatus { get; set; }
//    public string RoleName { get; set; }
//}

