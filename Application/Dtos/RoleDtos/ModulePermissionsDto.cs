namespace Application.Dtos.RoleDtos;

public record ModulePermissionsDto(
    int ModuleId,
    int PermissionId);

//public record RoleFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Organization { get; set; }
//}