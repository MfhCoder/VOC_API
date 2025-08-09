using Microsoft.AspNetCore.Mvc;
using Application.Dtos.SurveyDelivery;
using Core.Entities;
using API.RequestHelpers;
using AutoMapper;
using Application.Specifications.SurveyDelivery;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyDeliveryController : BaseApiController
    {
        private readonly ISurveyDeliveryService _surveyDeliveryService;

        public SurveyDeliveryController(ISurveyDeliveryService surveyDeliveryService, IMapper mapper) : base(mapper)
        {
            _surveyDeliveryService = surveyDeliveryService;
        }

        [HttpGet("GetTotalSurveyStatistics")]
        public async Task<ActionResult<SurveyStatisticsDto>> GetTotalSurveyStatistics()
        {
            var stats = await _surveyDeliveryService.GetTotalSurveyStatisticsAsync();
            return Ok(stats);
        }

        [HttpGet("SurveyBatches")]
        public async Task<ActionResult<Pagination<SurveyBatchDto>>> GetSurveyBatches([FromQuery] SurveyBatchFilterParams filterParams)
        {
            var spec = new SurveyBatchSpecification(filterParams);

            return await CreatePagedResult<SurveyBatch, SurveyBatchDto>(
                _surveyDeliveryService.GetSurveyBatch(),
                spec,
                filterParams.PageIndex,
                filterParams.PageSize);
        }

        [HttpGet("GetAllBatches")]
        public async Task<ActionResult> GetAllBatches([FromQuery] SurveyLogFilterParams filterParams)
        {
            var spec = new SurveyBatchSpecification(filterParams);
            return await CreatePagedResult<SurveyBatch, SurveyBatchDto>(
                _surveyDeliveryService.GetSurveyBatch(),
                spec,
                filterParams.PageIndex,
                filterParams.PageSize);
        }

        [HttpGet("{surveyId}/statistics")]
        public async Task<ActionResult<SurveyStatisticsDto>> GetSurveyStatistics(int surveyId)
        {
            var surveyStatistics = await _surveyDeliveryService.GetSurveyStatisticsAsync(surveyId);
            if (surveyStatistics == null)
                return NotFound($"Survey with ID {surveyId} not found");
            return Ok(surveyStatistics);
        }
        [HttpGet("export")]
        public async Task<IActionResult> ExportAllBatches([FromQuery] SurveyLogFilterParams filterParams)
        {
            var fileContent = await _surveyDeliveryService.ExportAllBatchesCSV(filterParams);
            return File(fileContent, "text/csv", "survey_batches.csv");
        }

    }
}
