//using API.RequestHelpers;
//using Application.Dtos.RoleDtos;
//using Application.Dtos.UserDtos;
//using Application.Interfaces;
//using Application.Specifications.Users;
//using AutoMapper;
//using Core.Entities;
//using Infrastructure.Data;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Net;
//namespace API.Controllers;

////[Authorize]
//[ApiController]
//[Route("api/[controller]")]
//public class RolesController : BaseApiController
//{
//    private readonly IRoleService _roleService;
//    private readonly IUnitOfWork unit;

//    public RolesController(IRoleService roleService,
//        IUnitOfWork unitOfWork,
//      IMapper mapper) : base(mapper)
//    {
//        _roleService = roleService;
//        unit = unitOfWork;
//    }

//    [HttpGet]
//    public async Task<ActionResult<Pagination<Role>>> GetRoles([FromQuery] RoleFilterParams filterParams)
//    {
//        var spec = new RoleSpecification(filterParams);

//        return await CreatePagedResult<Role, RolesDto>(
//         unit.Repository<Role>(),
//         spec,
//         filterParams.PageIndex,
//         filterParams.PageSize);
//    }

//    [HttpGet("{id}")]
//    public async Task<ActionResult<RolesDto>> GetRole(int id)
//    {
//        var role = await _roleService.GetRoleByIdAsync(id);
//        return Ok(role);
//    }

//    [HttpPost]
//    public async Task<ActionResult<CreateRoleDto>> CreateRole(CreateRoleDto createRoleDto)
//    {
//        return Ok(await _roleService.CreateRoleAsync(createRoleDto));
//    }

//    [HttpPut]
//    public async Task<ActionResult<UpdateRoleDto>> UpdateRole(UpdateRoleDto updateRoleDto)
//    {
//        return Ok(await _roleService.UpdateRoleAsync(updateRoleDto));
//    }

//    [HttpDelete("{id}")]
//    public async Task<ActionResult> DeleteRole(int id)
//    {

//        return Ok(await _roleService.DeleteRoleAsync(id));
//    }

//    [HttpGet("permissions")]
//    public async Task<ActionResult> GetPermissions()
//    {
//        var permissions = await unit.Repository<Permission>().ListAllAsync(q => q.Include(x => x.Survey).Include(s=>s.Module));
//        var res = _mapper.Map<IReadOnlyList<Permission>, IReadOnlyList<PermissionDto>>(permissions).GroupBy(s => s.ModuleName);
//        return Ok(res);
//    }

//    [HttpPost("CreatePermission")]
//    public async Task<ActionResult<PermissionDto>> CreatePermission([FromBody] CreatePermissionDto createPermissionDto)
//    {
//        var permission = _mapper.Map<CreatePermissionDto, Permission>(createPermissionDto);

//        unit.Repository<Permission>().Add(permission);

//        var result = await unit.Complete();

//        if (!result) return BadRequest("Problem creating permission");

//        return Ok(permission);
//    }

//    [HttpGet("GetModules")]
//    public async Task<ActionResult<IReadOnlyList<ModulesDto>>> GetModules()
//    {
//        var modules = await unit.Repository<Modules>().ListAllAsync();
//        return Ok(_mapper.Map<IReadOnlyList<Modules>, IReadOnlyList<ModulesDto>>(modules));
//    }

//}

using API.Controllers;
using API.RequestHelpers;
using Application.Dtos.RoleDtos;
using Application.Dtos.UserDtos;
using Application.Specifications.Roles;
using Application.Specifications.Users;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RolesController : BaseApiController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService, IMapper mapper) : base(mapper)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<Role>>> GetRoles([FromQuery] RoleFilterParams filterParams)
    {
        var spec = new RoleSpecification(filterParams);

        return await CreatePagedResult<Role, RolesDto>(
         _roleService.GetRolesAsync(),
         spec,
         filterParams.PageIndex,
         filterParams.PageSize);
    }

    //[HttpGet]
    //public async Task<ActionResult<Pagination<Role>>> GetRoles([FromQuery] RoleFilterParams filterParams)
    //{
    //    return await _roleService.GetRolesAsync(filterParams);
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<RolesDto>> GetRole(int id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<CreateRoleDto>> CreateRole(CreateRoleDto createRoleDto)
    {
        return Ok(await _roleService.CreateRoleAsync(createRoleDto));
    }

    [HttpPut]
    public async Task<ActionResult<UpdateRoleDto>> UpdateRole(UpdateRoleDto updateRoleDto)
    {
        return Ok(await _roleService.UpdateRoleAsync(updateRoleDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRole(int id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        if (!result) return NotFound("Role not found");
        return Ok();
    }

    [HttpGet("permissions")]
    public async Task<ActionResult> GetPermissions()
    {
        var res = await _roleService.GetGroupedPermissionsAsync();
        return Ok(res);
    }

    [HttpPost("CreatePermission")]
    public async Task<ActionResult<PermissionDto>> CreatePermission([FromBody] CreatePermissionDto createPermissionDto)
    {
        var result = await _roleService.CreatePermissionAsync(createPermissionDto);
        if (result == null) return BadRequest("Problem creating permission");
        return Ok(result);
    }

    [HttpGet("GetModules")]
    public async Task<ActionResult<IReadOnlyList<ModulesDto>>> GetModules()
    {
        var modules = await _roleService.GetModulesAsync();
        return Ok(modules);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportUsers([FromQuery] RoleFilterParams specParams)
    {
        return File(await _roleService.ExportRoleCSV(specParams), "text/csv", "roles.csv");
    }
}
