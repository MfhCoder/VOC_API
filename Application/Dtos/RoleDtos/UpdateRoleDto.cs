namespace Application.Dtos.RoleDtos;

public record UpdateRoleDto(
    string Name,
    List<AssignPermissionsDto> Permissions);

//public record RoleFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Organization { get; set; }
//}