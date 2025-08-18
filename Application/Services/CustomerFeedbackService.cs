using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Application.Interfaces;
using AutoMapper;
using Application.Dtos.CustomerFeedback;
using Application.Specifications.CustomerFeedback;
using Core.Entities;
using QuestPDF.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace Application.Services;

public class CustomerFeedbackService : ICustomerFeedbackService
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;
    private readonly ICustomerFeedbackRpo _feedbackRepo;

    public CustomerFeedbackService(IUnitOfWork uow, IMapper mapper, ICustomerFeedbackRpo feedbackRepo)
    {
        _unit = uow;
        _mapper = mapper;
        this._feedbackRepo = feedbackRepo;
    }
    public IGenericRepository<Feedback> GetAllAsync() => _unit.Repository<Feedback>();

    public async Task<FeedbackDetailsDto> GetDetailsByIdAsync(int feedbackId)
    {
        return await _feedbackRepo.GetFeedbackDetailsAsync(feedbackId);
    }
    public async Task<bool> EditFeedbackAsync(int feedbackId, EditFeedbackDto dto)
    {
        var feedbackSpec = new CustomerFeedbackSpecification(feedbackId);
        var feedback = await _unit.Repository<Feedback>().GetEntityWithSpec(feedbackSpec);

        if (feedback == null) return false;

        feedback.Sentiment= dto.Sentiment;

        feedback.FeedbackTags.Clear();
        foreach (var tagId in dto.TagIds)
        {
            feedback.FeedbackTags.Add(new FeedbackTag { FeedbackId = feedbackId, TagId = tagId });
        }

        await _unit.Complete();
        return true;
    }
    //public async Task<CustomerFeedbackDto> GetAllAsync(CustomerFeedbackFilterParams filter)
    //{
    //    var spec = new CustomerFeedbackSpecification(filter);
    //    var countSpec = new CustomerFeedbackSpecification(filter); // For count, could be optimized

    //    var totalItems = await _unit.Repository<Feedback>().CountAsync(countSpec);
    //    var feedbacks = await _unit.Repository<Feedback>().ListAsync(spec, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);

    //    var data = _mapper.Map<IReadOnlyList<CustomerFeedbackDto>>(feedbacks);

    //    return new CustomerFeedbackDto(filter.PageIndex, filter.PageSize, totalItems, data);
    //}
}