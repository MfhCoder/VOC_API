using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDtos;

public record CreateUserDto(
    [Required] string Name,
    [Required][EmailAddress] string Email,
    [Required] string Mobile,
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "RoleId must be greater than 0")]
    int RoleId,
    [Required] UserStatus Status);
