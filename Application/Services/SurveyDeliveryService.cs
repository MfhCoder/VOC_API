using Application.Dtos.SurveyDelivery;
using Application.Interfaces;
using Application.Specifications.SurveyDelivery;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Text;

public class SurveyDeliveryService : ISurveyDeliveryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IBackgroundJobManager _jobManager;
    public SurveyDeliveryService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IBackgroundJobManager jobManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _config = config;
        _jobManager = jobManager;
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
    public async Task<int> CreateSurveyBatchAsync(SurveyBatchCreateDto dto)
    {
        var batch = new SurveyBatch
        {
            SurveyId = dto.SurveyId,
            ChannelId = dto.ChannelId,
            ScheduledTime = dto.ScheduledTime ?? DateTime.UtcNow.AddMinutes(1),
            MerchantCount = dto.Merchants.Count,
            CreatedBy = 1
        };
        batch.CreatedAt = DateTime.UtcNow;//???

        var deliveries = new List<SurveyDelivery>();
        // Prepare Deliveries
        foreach (var merchantId in dto.Merchants)
        {
            // Generate encryption token: Encrypt(MerchantId + "-" + SurveyId)
            var plainToken = $"{merchantId}-{dto.SurveyId}";
            var encryptedToken = EncryptionHelper.Encrypt(plainToken);
            var siteUrl = _config["SiteURL"];
            deliveries.Add(new SurveyDelivery {
                DeliveryTime = batch.ScheduledTime,
                LinkExpirationDate = batch.ScheduledTime.AddDays(7),
                CreatedAt = DateTime.UtcNow,//???
                EncryptionToken = encryptedToken,
                MerchantId = merchantId,
                Status = "Pending",
                SurveyId = dto.SurveyId,
                SurveyURL = $"{siteUrl}{encryptedToken}",
            }); 
        }
        batch.SurveyDelivery = deliveries;
        _unitOfWork.Repository<SurveyBatch>().Add(batch);
        await _unitOfWork.Complete();

        // Schedule the job using Hangfire
        if (dto.ScheduledTime.HasValue && dto.ScheduledTime.Value > DateTime.UtcNow)
        {
            var batchDescription = $"Send Survey Batch ID: {batch.Id} - MerchantCount: {dto.Merchants.Count}";
            _jobManager.Schedule<ISurveyDeliveryService>(
                        x => x.SendSurveyBatchAsync(batch.Id, batchDescription),
                        batch.ScheduledTime
                    );
        }
        else
        {
            // Send immediately if no scheduled time or time is in the past
            await SendSurveyBatchAsync(batch.Id,null);
        }
        return batch.Id;
    }

    //[DisplayName("{1}")]
    public async Task SendSurveyBatchAsync(int batchId, string? batchDescription)
    {
        // Implement the logic to send out the survey batch (e.g., send emails, SMS, etc.)
        // This method must be public and serializable by Hangfire.
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
    public async Task<int> ResendUndeliveredSurveysAsync(int batchId)
    {
        var batch = await _unitOfWork.Repository<SurveyBatch>()
            .GetByIdAsync(batchId, b => b.Include(s=>s.SurveyDelivery)); 

        if (batch == null || batch.SurveyDelivery == null)
            return 0;

        var undelivered = batch.SurveyDelivery
            .Where(sd => sd.Status == "Undelivered")
            .ToList();

        foreach (var delivery in undelivered)
        {
            delivery.Status = "Resent"; 
            delivery.LinkExpirationDate = DateTime.UtcNow.AddDays(7);
            delivery.UpdatedAt = DateTime.UtcNow;
            delivery.UpdatedBy = 1;//???
            delivery.RetryCount += 1;//???
        }

        await _unitOfWork.Complete();
        await SendSurveyBatchAsync(batch.Id, null);
        return undelivered.Count;
    }
}
