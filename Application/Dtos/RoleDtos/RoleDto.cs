namespace Application.Dtos.RoleDtos;

public record RoleDto(
    int Id,
    string Name,
    DateTime CreatedDate,
    List<AssignPermissionsDto> Permissions);

//public record RoleFilterParams : PaginationParams
//{
//    public string Search { get; set; }
//    public string Organization { get; set; }
//}