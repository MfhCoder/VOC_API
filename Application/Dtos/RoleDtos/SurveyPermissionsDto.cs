namespace Application.Dtos.RoleDtos;

public record SurveyPermissionsDto(
    int SurveyId,
    int PermissionId);

//public record RoleFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Organization { get; set; }
//}