using API.RequestHelpers;
using Application.Dtos.CustomerFeedback;
using Application.Dtos.RoleDtos;
using Application.Interfaces;
using Application.Services;
using Application.Specifications.CustomerFeedback;
using Application.Specifications.Roles;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerFeedbackController : BaseApiController
    {
        private readonly ICustomerFeedbackService _service;

        public CustomerFeedbackController(ICustomerFeedbackService service, IMapper mapper) : base(mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<CustomerFeedbackDto>>> Get([FromQuery] CustomerFeedbackFilterParams filter)
        {
            var spec = new CustomerFeedbackSpecification(filter);

            return await CreatePagedResult<Feedback, CustomerFeedbackDto>(
             _service.GetAllAsync(),
             spec,
             filter.PageIndex,
             filter.PageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDetailsDto>> GetDetails(int id)
        {
            var details = await _service.GetDetailsByIdAsync(id);
            if (details == null)
                return NotFound();
            return Ok(details);
        }

        /// <summary>
        /// Edit feedback tags and sentiment.
        /// </summary>
        /// <param name="id">Feedback Id</param>
        /// <param name="dto">EditFeedbackDto: tags[], sentiment (true=Positive, false=Negative)</param>
        [HttpPut("{id}")]
        public async Task<ActionResult> EditFeedback(int id, [FromBody] EditFeedbackDto dto)
        {
            var result = await _service.EditFeedbackAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
