using Microsoft.AspNetCore.Mvc;
using Application.DTOs.SurveyBuilder;
using Application.Interfaces;
using Application.Dtos.SurveyBuilder;
using Core.Entities;
using API.RequestHelpers;
using Application.Specifications.SurveyBuilder;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyBuilderController : BaseApiController
    {
        private readonly ISurveyBuilderService _surveyBuilderService;

        public SurveyBuilderController(ISurveyBuilderService surveyBuilderService, IMapper mapper) : base(mapper)
        {
            _surveyBuilderService = surveyBuilderService;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<SurveyDto>>> GetSurveys([FromQuery] SurveyFilterParams filterParams)
        {
            var spec = new SurveySpecification(filterParams);

            return await CreatePagedResult<Survey, SurveyDto>(
                _surveyBuilderService.GetSurveysAsync(),
                spec,
                filterParams.PageIndex,
                filterParams.PageSize);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyDto dto)
        {
            try
            {
                var surveyId = await _surveyBuilderService.CreateSurveyAsync(dto);
                return CreatedAtAction(nameof(GetSurvey), new { surveyId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{surveyId}")]
        public async Task<IActionResult> GetSurvey(int surveyId)
        {
            try
            {
                var survey = await _surveyBuilderService.GetSurveyAsync(surveyId);
                return Ok(survey);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{surveyId}")]
        public async Task<IActionResult> UpdateSurvey(int surveyId, [FromBody] CreateSurveyDto dto)
        {
            try
            {
                await _surveyBuilderService.UpdateSurveyAsync(surveyId, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{surveyId}")]
        public async Task<IActionResult> DeleteSurvey(int surveyId)
        {
            try
            {
                await _surveyBuilderService.DeleteSurveyAsync(surveyId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("questions/branches")]
        public async Task<IActionResult> AddQuestionBranch([FromBody] CreateQuestionBranchDto dto)
        {
            try
            {
                await _surveyBuilderService.AddQuestionBranchAsync(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("SaveFilters")]
        public async Task<IActionResult> SaveFilters([FromBody] SurveyFiltersDto dto)
        {
            var id = await _surveyBuilderService.SaveSurveyFilterAsync(dto);
            return Ok(id);
        }


        [HttpPut("UpdateFilters")]
        public async Task<IActionResult> UpdateFilters([FromBody] SurveyFiltersDto dto)
        {
            var id = await _surveyBuilderService.UpdateSurveyFilterAsync(dto);
            return Ok(id);
        }

        [HttpGet("GetFilter/{surveyId}")]
        public async Task<ActionResult<SurveyFiltersDto>> GetFilter(int surveyId)
        {
            try
            {
                var dto = await _surveyBuilderService.GetSurveyFilterAsync(surveyId);
                return Ok(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("SaveSurveySettings")]
        public async Task<IActionResult> SaveSurveySettings([FromBody] SurveySettingsDto dto)
        {
            var id = await _surveyBuilderService.SaveSurveySettingsAsync(dto);
            return Ok(id);
        }

        [HttpGet("QuestionTypes")]
        public async Task<ActionResult<IReadOnlyList<QuestionType>>> GetQuestionTypes()
        {
            return Ok(await _surveyBuilderService.GetQuestionTypesAsync());
        }
    }
}
