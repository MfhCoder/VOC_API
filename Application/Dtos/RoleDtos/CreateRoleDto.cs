using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RoleDtos;

public record CreateRoleDto(
    [Required] string Name,
    List<int> PermissionIds
    );
