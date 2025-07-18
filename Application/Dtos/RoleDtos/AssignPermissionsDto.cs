namespace Application.Dtos.RoleDtos;

public record AssignPermissionsDto(
    List<ModulePermissionsDto> ModulePermissionIds,
    List<SurveyPermissionsDto> SurveyPermissionIds);

//public record RoleFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Organization { get; set; }
//}