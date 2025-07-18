using Application.Dtos.UserDtos;
using Application.DTOs;
using Application.Specifications;
using Core.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int userId);
    public Task<CreateUserResult> CreateUserAsync(CreateUserDto createDto);
    Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
    public Task<bool> ChangeUserStatusAsync(int userId, UserStatus status);
    public Task<byte[]> ExportUsersCSV(UserFilterParams specParams);
}

