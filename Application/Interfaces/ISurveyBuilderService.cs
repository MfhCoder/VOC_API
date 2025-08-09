using Application.Dtos.SurveyBuilder;
using Application.DTOs.SurveyBuilder;
using Core.Entities;

namespace Application.Interfaces
{
    public interface ISurveyBuilderService
    {
        Task<IReadOnlyList<QuestionTypeDto>> GetQuestionTypesAsync();
        Task<int> CreateSurveyAsync(CreateSurveyDto dto);
        Task<GetSurveyDto> GetSurveyAsync(int surveyId);
        Task UpdateSurveyAsync(int surveyId, CreateSurveyDto dto);
        Task DeleteSurveyAsync(int surveyId);
        Task AddQuestionBranchAsync(CreateQuestionBranchDto dto);
        Task<int> SaveSurveyFilterAsync(SurveyFiltersDto dto);
        Task<int> UpdateSurveyFilterAsync(SurveyFiltersDto dto);
        Task<SurveyFiltersDto> GetSurveyFilterAsync(int SurveyId);
        Task<int> SaveSurveySettingsAsync(SurveySettingsDto dto);
        IGenericRepository<Survey> GetSurveysAsync();

    }
}
