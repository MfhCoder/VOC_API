using Application.Dtos.RoleDtos;
using Application.Dtos.UserDtos;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Roles;
using Application.Specifications.Users;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork _unitOfWork, IMapper mapper)
    {
        _unit = _unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateRoleDto> CreateRoleAsync(CreateRoleDto createDto)
    {
        var role = _mapper.Map<Role>(createDto);

        foreach (var permissionId in createDto.PermissionIds)
        {

            var rolePermission = new RolePermission
            {
                PermissionId = permissionId
            };
            role.RolePermissions.Add(rolePermission);
        }

        _unit.Repository<Role>().Add(role);
        var result = await _unit.Complete();

        return createDto;
    }

    public async Task<UpdateRoleDto> UpdateRoleAsync(UpdateRoleDto updateUserDto)
    {
        var role = await _unit.Repository<Role>().GetByIdAsync(
                                             updateUserDto.Id,
                                             q => q.Include(x => x.RolePermissions));
        role.RolePermissions.Clear();

        foreach (var permissionId in updateUserDto.PermissionIds)
        {

            var rolePermission = new RolePermission
            {
                PermissionId = permissionId
            };
            role.RolePermissions.Add(rolePermission);
        }

        _unit.Repository<Role>().Update(role);
        var result = await _unit.Complete();

        return updateUserDto;
    }

    public async Task<bool> DeleteRoleAsync(int Id)
    {
        var role = await _unit.Repository<Role>().GetByIdAsync(Id);

        if (role == null) return false;

        await _unit.Repository<Role>().DeleteAsync(Id);

        return await _unit.Complete();
    }

    public async Task<byte[]> ExportRoleCSV(RoleFilterParams specParams)
    {
        var spec = new RoleSpecification(specParams);
        var roles = await _unit.Repository<Role>().ListAsync(spec);

        var sb = new StringBuilder();
        sb.AppendLine("RoleId,RoleName,CreatedDate");

        foreach (var role in roles)
        {
            sb.AppendLine($"{role.Id},{role.Name},{role.CreatedDate}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public async Task<RoleDto> GetRoleByIdAsync(int roleId)
    {
        var spec = new RoleSpecification(roleId);

        var role = await _unit.Repository<Role>().GetEntityWithSpec(spec);
        if (role == null)
            return null;
        var roleDto = _mapper.Map<RoleDto>(role);

        if (role.RolePermissions.Any())
        {
            foreach (var rolePermission in role.RolePermissions)
            {
                var permission = await _unit.Repository<Permission>().GetByIdAsync(
                                             rolePermission.PermissionId,
                                             q => q.Include(x => x.Module).Include(x => x.Survey));

                if (permission.ModuleId != null)
                {
                    roleDto.ModulePermissions.Add(new ModulePermissionsDto
                    {
                        PermissionId = rolePermission.Id,
                        ModuleId = permission.ModuleId,
                        ModuleName = permission.Module.Name,
                        PermissionName = permission.Name
                    });
                }
                else
                if (permission.SurveyId != null)
                {
                    roleDto.SurveyPermissions.Add(new SurveyPermissionsDto
                    {
                        PermissionId = rolePermission.Id,
                        SurveyId = permission.SurveyId,
                        SurveyName = rolePermission.Permission.Survey.Name,
                        PermissionName = permission.Name
                    });
                }

            }
        }

        return roleDto;
    }

    public IGenericRepository<Role> GetRolesAsync() => _unit.Repository<Role>();
    public async Task<IEnumerable<IGrouping<string, PermissionDto>>> GetGroupedPermissionsAsync()
    {
        var permissions = await _unit.Repository<Permission>()
            .ListAllAsync(q => q.Include(x => x.Survey).Include(s => s.Module));
        var res = _mapper.Map<IReadOnlyList<Permission>, IReadOnlyList<PermissionDto>>(permissions)
            .GroupBy(s => s.ModuleName);
        return res;
    }

    public async Task<PermissionDto?> CreatePermissionAsync(CreatePermissionDto createPermissionDto)
    {
        var permission = _mapper.Map<CreatePermissionDto, Permission>(createPermissionDto);
        _unit.Repository<Permission>().Add(permission);
        var result = await _unit.Complete();
        if (!result) return null;
        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task<IReadOnlyList<ModulesDto>> GetModulesAsync()
    {
        var modules = await _unit.Repository<Modules>().ListAllAsync();
        return _mapper.Map<IReadOnlyList<Modules>, IReadOnlyList<ModulesDto>>(modules);
    }

}