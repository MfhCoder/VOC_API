using Application.Dtos.SurveyDelivery;
using Application.Interfaces;
using Application.Specifications.SurveyDelivery;
using AutoMapper;
using Core.Entities;
using System.Text;

public class SurveyDeliveryService : ISurveyDeliveryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SurveyDeliveryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IGenericRepository<SurveyBatch> GetSurveyBatch() => _unitOfWork.Repository<SurveyBatch>();
    public async Task<SurveyStatisticsDto> GetSurveyStatisticsAsync(int id)
    {
        var spec = new SurveyWithDetailsSpecification(id);
        var survey = await _unitOfWork.Repository<Survey>().GetEntityWithSpec(spec);
        var surveysSent = survey.SurveyDelivery.Count();
        var surveysDelivered = survey.SurveyDelivery.Where(s => s.Status == "Delivered").Count();
        var surveysResponses = survey.Feedbacks.Count();
        decimal responseRate = (surveysSent > 0) ? ((decimal)surveysResponses / surveysSent) * 100 : 0;

        var statistics = new SurveyStatisticsDto
        {
            SurveysSent = surveysSent,
            SurveysDelivered = surveysDelivered,
            SurveysResponses = surveysResponses,
            ResponseRate = responseRate
        };

        return statistics;
    }
    public async Task<SurveyStatisticsDto> GetTotalSurveyStatisticsAsync()
    {
        var spec = new SurveyBatchSpecification();
        var batches = await _unitOfWork.Repository<SurveyBatch>().ListAsync(spec);
        var surveysSent = batches.Sum(b => b.SurveyDelivery.Count());
        var surveysDelivered = batches.Sum(b => b.SurveyDelivery.Where(s => s.Status == "Delivered").Count());
        var surveysResponses = batches.Sum(b => b.Survey.Feedbacks.Count());
        decimal responseRate = (surveysSent > 0) ? ((decimal)surveysResponses / surveysSent) * 100 : 0;

        var statistics = new SurveyStatisticsDto
        {
            SurveysSent = surveysSent,
            SurveysDelivered = surveysDelivered,
            SurveysResponses = surveysResponses,
            ResponseRate = responseRate
        };

        return statistics;
    }
    public async Task<byte[]> ExportAllBatchesCSV(SurveyLogFilterParams filterParams)
    {
        var spec = new SurveyBatchSpecification(filterParams);
        var batches = await _unitOfWork.Repository<SurveyBatch>().ListAsync(spec);
        var dtos = _mapper.Map<List<SurveyBatchDto>>(batches);

        var sb = new StringBuilder();
        sb.AppendLine("BatchId,SurveyId,SurveyName,ChannelId,ScheduledTime,ChannelName,MerchantCount,CreatedBy,CreatedAt,SurveysSent,SurveysDelivered,DeliveryRate,SurveysResponses,ResponseRate"); 

        foreach (var batch in dtos)
        {
            sb.AppendLine($"{batch.BatchId},{batch.SurveyId},{batch.SurveyName},{batch.ChannelId},{batch.ScheduledTime},{batch.ChannelName},{batch.MerchantCount},{batch.CreatedBy},{batch.CreatedAt},{batch.SurveysSent},{batch.SurveysDelivered},{batch.DeliveryRate},{batch.SurveysResponses},{batch.ResponseRate}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

}
