using Application.Dtos.UserDtos;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Users;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using QuestPDF.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork unit;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
    {
        unit = unitOfWork;
        _mapper = mapper;
        _emailService = emailService;
    }

    //public async Task<Pagination<UserDto>> GetUsersAsync(UserFilterParams filterParams)
    //{
    //    var spec = new UserSpecification(filterParams);
    //    var countSpec = new UserSpecification(filterParams);

    //    var totalItems = await _unitOfWork.Repository<User>().CountAsync(countSpec);
    //    var users = await _unitOfWork.Repository<User>().ListAsync(spec);

    //    var data = _mapper.Map<IReadOnlyList<UserDto>>(users);

    //    return new Pagination<UserDto>(
    //        filterParams.PageNumber,
    //        filterParams.PageSize,
    //        totalItems,
    //        data);
    //}

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        var spec = new UserSpecification(userId);

        var user = await unit.Repository<User>().GetEntityWithSpec(spec);
        return _mapper.Map<UserDto>(user);
    }


    public async Task<CreateUserResult> CreateUserAsync(CreateUserDto createDto)
    {
        var user = _mapper.Map<User>(createDto);

        unit.Repository<User>().Add(user);
        await unit.Complete();

        string? warning = null;

        try
        {
            await _emailService.SendUserInvitationAsync(user.Email, user.Name);
        }
        catch (Exception ex)
        {
            warning = "User created, but failed to send invitation email.";
        }

        return new CreateUserResult
        {
            User = _mapper.Map<UserDto>(user),
            Warning = warning
        };
    }

    public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateDto)
    {
        var user = await unit.Repository<User>().GetByIdAsync(userId);
        _mapper.Map(updateDto, user);

        unit.Repository<User>().Update(user);
        await unit.Complete();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> ChangeUserStatusAsync(int userId, UserStatus status)
    {
        var user = await unit.Repository<User>().GetByIdAsync(userId);
        user.Status = status;

        unit.Repository<User>().Update(user);
        return await unit.Complete();
    }
    public StringBuilder GenerateCsv(IReadOnlyList<UserDto> merchants)
    {
        var csv = new StringBuilder();
       
        csv.AppendLine("Id,Name,Email,Mobile,JoiningDate,Status,Role");

        foreach (var m in merchants)
        {
            csv.AppendLine($"{m.Id},{CsvHelper.EscapeCsv(m.Name)},{CsvHelper.EscapeCsv(m.Email)},{CsvHelper.EscapeCsv(m.Mobile)},{CsvHelper.EscapeCsv(m.JoiningDate.ToString())},{m.Status},{m.RoleName}");
        }

        return csv;
    }
    public async Task<byte[]> ExportUsersCSV(UserFilterParams specParams)
    {
     
        var spec = new UserSpecification(specParams);
        var user = await unit.Repository<User>().ListAsync(spec);
        var userDto = _mapper.Map<IReadOnlyList<UserDto>>(user);
        var csv = GenerateCsv(userDto);

        var bytes = Encoding.UTF8.GetBytes(csv.ToString());
        return bytes;
    }
}