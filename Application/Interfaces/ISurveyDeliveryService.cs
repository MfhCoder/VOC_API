using Application.Dtos.SurveyDelivery;
using Application.Interfaces;
using Core.Entities;

public interface ISurveyDeliveryService
{
    public IGenericRepository<SurveyBatch> GetSurveyBatch();
    Task<SurveyStatisticsDto> GetSurveyStatisticsAsync(int id);
    Task<SurveyStatisticsDto> GetTotalSurveyStatisticsAsync();
    Task<byte[]> ExportAllBatchesCSV(SurveyLogFilterParams filterParams);

}
