using Application.Dtos.SurveyBuilder;
using Application.DTOs.SurveyBuilder;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.SurveyBuilder;
using AutoMapper;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;


namespace Application.Services
{
    public class SurveyBuilderService : ISurveyBuilderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SurveyBuilderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IGenericRepository<Survey> GetSurveysAsync() => _unitOfWork.Repository<Survey>();

        public async Task<int> CreateSurveyAsync(CreateSurveyDto dto)
        {
            var survey = _mapper.Map<Survey>(dto);
            survey.CreatedAt = DateTime.UtcNow;

            // Map Sections and Questions
            if (dto.Sections != null)
            {
                survey.QuestionSections = new List<QuestionSection>();
                foreach (var sectionDto in dto.Sections)
                {
                    var section = _mapper.Map<QuestionSection>(sectionDto);
                    section.Survey = survey;
                    section.Questions = new List<SurveyQuestion>();

                    if (sectionDto.Questions != null)
                    {
                        foreach (var qDto in sectionDto.Questions)
                        {
                            var question = _mapper.Map<SurveyQuestion>(qDto);
                            question.Survey = survey;
                            question.Section = section;

                            // Map QuestionOptions
                            if (qDto.Options != null && qDto.Options.Any())
                            {
                                question.Options = new List<QuestionOption>();
                                foreach (var optionDto in qDto.Options)
                                {
                                    var option = _mapper.Map<QuestionOption>(optionDto);
                                    option.Question = question;
                                    question.Options.Add(option);
                                }
                            }

                            section.Questions.Add(question);
                            //survey.Questions.Add(question);
                        }
                    }
                    survey.QuestionSections.Add(section);
                }
            }

            try
            {
                _unitOfWork.Repository<Survey>().Add(survey);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {

                throw;
            }

            return survey.Id;
        }

        public async Task<GetSurveyDto> GetSurveyAsync(int surveyId)
        {
            var spec = new SurveyWithSectionsAndQuestionsSpecification(surveyId);
            var survey = await _unitOfWork.Repository<Survey>().GetEntityWithSpec(spec);

            if (survey == null) throw new KeyNotFoundException("Survey not found.");

            var dto = _mapper.Map<GetSurveyDto>(survey);
            return dto;
        }

        public async Task UpdateSurveyAsync(int surveyId, CreateSurveyDto dto)
        {

            var spec = new SurveyWithSectionsAndQuestionsSpecification(surveyId);
            var survey = await _unitOfWork.Repository<Survey>().GetEntityWithSpec(spec);

            if (survey == null) throw new KeyNotFoundException("Survey not found.");

            survey.Name = dto.Name;

            // Remove existing sections and questions (including options)
            foreach (var section in survey.QuestionSections)
            {
                foreach (var question in section.Questions)
                {
                    if (question.Options != null && question.Options.Any())
                        _unitOfWork.Repository<QuestionOption>().DeleteRange(question.Options);
                }
                if (section.Questions != null && section.Questions.Any())
                    _unitOfWork.Repository<SurveyQuestion>().DeleteRange(section.Questions);
            }
            if (survey.QuestionSections != null && survey.QuestionSections.Any())
                _unitOfWork.Repository<QuestionSection>().DeleteRange(survey.QuestionSections);

            survey.QuestionSections = new List<QuestionSection>();

            // Re-add sections, questions, and options
            if (dto.Sections != null)
            {
                foreach (var sectionDto in dto.Sections)
                {
                    var section = _mapper.Map<QuestionSection>(sectionDto);
                    section.Survey = survey;
                    section.Questions = new List<SurveyQuestion>();

                    if (sectionDto.Questions != null)
                    {
                        foreach (var qDto in sectionDto.Questions)
                        {
                            var question = _mapper.Map<SurveyQuestion>(qDto);
                            question.Survey = survey;
                            question.Section = section;

                            // Map QuestionOptions
                            if (qDto.Options != null && qDto.Options.Any())
                            {
                                question.Options = new List<QuestionOption>();
                                foreach (var optionDto in qDto.Options)
                                {
                                    var option = _mapper.Map<QuestionOption>(optionDto);
                                    option.Question = question;
                                    question.Options.Add(option);
                                }
                            }

                            section.Questions.Add(question);
                        }
                    }
                    survey.QuestionSections.Add(section);
                }
            }

            _unitOfWork.Repository<Survey>().Update(survey);
            await _unitOfWork.Complete();
        }

        public async Task DeleteSurveyAsync(int surveyId)
        {
            var spec = new SurveyWithSectionsAndQuestionsSpecification(surveyId);
            var survey = await _unitOfWork.Repository<Survey>().GetEntityWithSpec(spec);

            if (survey == null) throw new KeyNotFoundException("Survey not found.");

            // Delete all options, questions, and sections
            foreach (var section in survey.QuestionSections)
            {
                foreach (var question in section.Questions)
                {
                    if (question.Options != null && question.Options.Any())
                        _unitOfWork.Repository<QuestionOption>().DeleteRange(question.Options);
                }
                if (section.Questions != null && section.Questions.Any())
                    _unitOfWork.Repository<SurveyQuestion>().DeleteRange(section.Questions);
            }
            if (survey.QuestionSections != null && survey.QuestionSections.Any())
                _unitOfWork.Repository<QuestionSection>().DeleteRange(survey.QuestionSections);

            await _unitOfWork.Repository<Survey>().DeleteAsync(survey.Id);
            await _unitOfWork.Complete();
        }


        public async Task AddQuestionBranchAsync(CreateQuestionBranchDto dto)
        {
            var question = await _unitOfWork.Repository<SurveyQuestion>().GetByIdAsync(dto.questionId);
            if (question == null)
                throw new KeyNotFoundException("Question not found.");

            question.TriggerOptionId = dto.TriggerOptionId;
            _unitOfWork.Repository<SurveyQuestion>().Update(question);
            await _unitOfWork.Complete();
        }
        public async Task<int> SaveSurveyFilterAsync(SurveyFiltersDto dto)
        {
            var filter = _mapper.Map<SurveyFilters>(dto);
            _unitOfWork.Repository<SurveyFilters>().Add(filter);
            await _unitOfWork.Complete();
            return filter.Id;
        }

        public async Task<int> UpdateSurveyFilterAsync(SurveyFiltersDto dto)
        {
            //var SurveyFiltersUpdateSpecification = new SurveyFiltersUpdateSpecification(dto.SurveyId);
            //var SurveyFilters = await _unitOfWork.Repository<SurveyFilters>().GetEntityWithSpec(SurveyFiltersUpdateSpecification);
            var SurveyFilters = _mapper.Map<SurveyFilters>(dto);
            _unitOfWork.Repository<SurveyFilters>().Update(SurveyFilters);
            await _unitOfWork.Complete();
            return SurveyFilters.Id;
        }

        public async Task<SurveyFiltersDto> GetSurveyFilterAsync(int SurveyId)
        {
            var SurveyFiltersUpdateSpecification = new SurveyFiltersUpdateSpecification(SurveyId);
            var SurveyFilters = await _unitOfWork.Repository<SurveyFilters>().GetEntityWithSpec(SurveyFiltersUpdateSpecification);
            if (SurveyFilters == null) throw new KeyNotFoundException("Survey filter not found.");
            var dto = _mapper.Map<SurveyFiltersDto>(SurveyFilters);
            return dto;
        }

        public async Task<int> SaveSurveySettingsAsync(SurveySettingsDto dto)
        {
            var survey = await _unitOfWork.Repository<Survey>().GetByIdAsync(dto.Id);
            survey.Type = dto.Type;
            survey.Status = dto.Status;
            survey.Connected_DB = dto.Connected_DB;
            survey.MessageContent = dto.MessageContent;
            await _unitOfWork.Complete();
            return dto.Id;
        }

        public async Task<IReadOnlyList<QuestionTypeDto>> GetQuestionTypesAsync()
        {
            var types = await _unitOfWork.Repository<QuestionType>().ListAllAsync();
            return _mapper.Map<List<QuestionTypeDto>>(types);
        }
    }
}