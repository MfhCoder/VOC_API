using Core.Entities;

namespace Application.Dtos.RoleDtos;

public class RoleDto {
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public DateTime CreatedDate { get; set; }
    //public List<RolePermission> RolePermissions,
    public List<ModulePermissionsDto> ModulePermissions { get; set; } = new List<ModulePermissionsDto>();
    public List<SurveyPermissionsDto> SurveyPermissions { get; set; } = new List<SurveyPermissionsDto>();
}


public class ModulePermissionsDto {
    public int? ModuleId { get; set; }
    public int PermissionId { get; set; }
    public string PermissionName { get; set; }
    public string ModuleName { get; set; }
};

public class SurveyPermissionsDto
{
    public int? SurveyId { get; set; }
    public int PermissionId { get; set; }
    public string PermissionName { get; set; }
    public string SurveyName { get; set; }
}
