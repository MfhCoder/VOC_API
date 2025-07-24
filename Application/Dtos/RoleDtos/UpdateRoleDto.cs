using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RoleDtos;

public record UpdateRoleDto(
    int Id,
    string Name,
    List<int> PermissionIds
    );
