namespace Application.Dtos.RoleDtos;

public record PermissionDto(
    int Id,
    string Name,
    int ModuleId,
    int SurveyId,
    string ModuleName,
    string SurveyName
    );
