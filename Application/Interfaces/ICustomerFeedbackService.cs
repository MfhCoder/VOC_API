using Application.Dtos.CustomerFeedback;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerFeedbackService
    {
        public IGenericRepository<Feedback> GetAllAsync();
        Task<FeedbackDetailsDto> GetDetailsByIdAsync(int feedbackId);
        Task<bool> EditFeedbackAsync(int feedbackId, EditFeedbackDto dto);
    }
}
