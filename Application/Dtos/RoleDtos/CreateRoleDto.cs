using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RoleDtos;

public record CreateRoleDto(
    [Required] string Name,
    List<AssignPermissionsDto> PermissionIds
    );

//public record RoleFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Organization { get; set; }
//}