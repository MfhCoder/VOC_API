using Application.Dtos.RoleDtos;
using Application.Interfaces;
using Core.Entities;

public interface IRoleService
{
       public IGenericRepository<Role> GetRolesAsync();
    Task<RoleDto> GetRoleByIdAsync(int id);
    Task<CreateRoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);
    Task<UpdateRoleDto> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
    Task<bool> DeleteRoleAsync(int id);
    Task<IEnumerable<IGrouping<string, PermissionDto>>> GetGroupedPermissionsAsync();
    Task<PermissionDto?> CreatePermissionAsync(CreatePermissionDto createPermissionDto);
    Task<IReadOnlyList<ModulesDto>> GetModulesAsync();
    Task<byte[]> ExportRoleCSV(RoleFilterParams specParams);

}
