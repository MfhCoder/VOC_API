using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RoleDtos;

public record AssignPermissionsDto(
    List<ModulePermissionIds> ModulePermissionIds,
    List<SurveyPermissionIds> SurveyPermissionIds);

public record ModulePermissionIds(
    int ModuleId,
    int PermissionId);

public record SurveyPermissionIds(
    int SurveyId,
    int PermissionId);
