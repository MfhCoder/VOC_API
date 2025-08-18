using Application.Dtos.CustomerFeedback;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CustomerFeedbackRpo : ICustomerFeedbackRpo
    {
        private readonly VocContext _context;

        public CustomerFeedbackRpo(VocContext context)
        {
            _context = context;
        }

        public async Task<FeedbackDetailsDto> GetFeedbackDetailsAsync(int feedbackId)
        {
            var Feedback = _context.Feedbacks
                .AsNoTracking()
                .Where(f => f.Id == feedbackId)
                .Include(f => f.Merchant)
                .Include(f => f.Survey)
                .Include(f => f.Delivery)
                .Include(f => f.FeedbackTags).ThenInclude(s => s.Tag)
                .Include(f => f.Answers)
                .ThenInclude(a => a.Question).FirstOrDefault();

            var FeedbackDto = new FeedbackDetailsDto {
                SurveyName = Feedback.Survey.Name,
                Sentiment = Feedback.Sentiment,
                MerchantId = Feedback.Merchant?.MerchantId,
                MerchantName = Feedback.Merchant?.Name,
                ResponseDate = Feedback.SubmittedAt,
                BatchId = Feedback.Delivery?.BatchId,
                Answers = Feedback.Answers.Select(a => new QuestionAnswerDto
                {
                    Question = a.Question.QuestionText,
                    Answer = a.ResponseText ?? (a.Option != null ? a.Option.OptionText : "No Answer")
                }).ToList(),
                FeedbackTags = Feedback.FeedbackTags.Count > 0 ? Feedback.FeedbackTags.Select(t => t.Tag.Name).ToList() : new List<string>()
            };
              
            return FeedbackDto;
        }
    }

}
