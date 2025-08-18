using Application.Dtos.SurveyDelivery;
using Application.Interfaces;
using Core.Entities;
using System.ComponentModel;

public interface ISurveyDeliveryService
{
    public IGenericRepository<SurveyBatch> GetSurveyBatch();
    Task<SurveyStatisticsDto> GetSurveyStatisticsAsync(int id);
    Task<SurveyStatisticsDto> GetTotalSurveyStatisticsAsync();
    Task<byte[]> ExportAllBatchesCSV(SurveyLogFilterParams filterParams);
    Task<int> CreateSurveyBatchAsync(SurveyBatchCreateDto dto);
    //public Task SendSurveyBatchAsync(int batchId);
    [DisplayName("{1}")]
    public Task SendSurveyBatchAsync(int batchId, string? batchDescription);
    Task<int> ResendUndeliveredSurveysAsync(int batchId);
}