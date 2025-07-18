using API.RequestHelpers;
using Application.Dtos.Merchant;
using Application.Dtos.UserDtos;
using Application.Interfaces;
using Application.Services;
using Application.Specifications;
using Application.Specifications.Users;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;

namespace API.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork unit;

    public UsersController(IUserService userService,
        IUnitOfWork unitOfWork,
      IMapper mapper) : base(mapper)
    {
        _userService = userService;
        unit = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<UserDto>>> GetUsers([FromQuery] UserFilterParams filterParams)
    {
        var spec = new UserSpecification(filterParams);

        return await CreatePagedResult<User, UserDto>(
         unit.Repository<User>(),
         spec,
         filterParams.PageIndex,
         filterParams.PageSize);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
    {
        var result = await _userService.CreateUserAsync(createUserDto);

        if (!string.IsNullOrEmpty(result.Warning))
        {
            return CreatedAtAction(
                nameof(GetUser),
                new { id = result.User.Id },
                new { result.User, result.Warning });
        }

        return CreatedAtAction(
            nameof(GetUser),
            new { id = result.User.Id },
            result.User);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var user = await _userService.UpdateUserAsync(id, updateUserDto);
        return Ok(user);
    }

    [HttpPatch("{id}/ChangeStatus")]
    public async Task<IActionResult> ChangeStatus(int id, UserStatus status)
    {
        await _userService.ChangeUserStatusAsync(id, status);
        return NoContent();
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportUsers([FromQuery] UserFilterParams specParams)
    {
        return File(await _userService.ExportUsersCSV(specParams), "text/csv", "users.csv");
    }
}