using Application.Dtos.RoleDtos;

namespace Application.Interfaces;

public interface IRoleService
{
    Task<RoleDto> GetRoleByIdAsync(int roleId);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);
    Task<RoleDto> UpdateRoleAsync(int roleId, UpdateRoleDto updateRoleDto);
    Task<bool> DeleteRoleAsync(int roleId);
}
