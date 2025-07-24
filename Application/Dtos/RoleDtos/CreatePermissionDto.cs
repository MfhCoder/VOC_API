using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RoleDtos;

public record CreatePermissionDto(
    [Required] 
    string Name,
    [Range(1, int.MaxValue, ErrorMessage = "ModuleId must be greater than 0.")] 
    int? ModuleId,
    [Range(1, int.MaxValue, ErrorMessage = "SurveyId must be greater than 0.")]
    int? SurveyId);
