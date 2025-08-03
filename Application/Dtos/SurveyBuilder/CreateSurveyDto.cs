using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.SurveyBuilder
{
    public class CreateSurveyDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? Connected_DB { get; set; }
        public bool? Status { get; set; }
        public string? MessageContent { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<QuestionSectionDto> Sections { get; set; }
    }

    public class QuestionSectionDto
    {
        public string Title { get; set; }
        public int SectionOrder { get; set; }
        public List<SurveyQuestionDto> Questions { get; set; }
    }
    public class SurveyQuestionDto
    {
        public string QuestionText { get; set; }
        public int QuestionOrder { get; set; }
        public bool IsRequired { get; set; }
        public int QuestionTypeId { get; set; }

        public List<QuestionOptionDto>? Options { get; set; }
    }

    public class QuestionOptionDto
    {
        public string OptionText { get; set; }
        public int OptionOrder { get; set; }
    }

    public class QuestionBranchDto
    {
        public int TriggerOptionId { get; set; }
        public List<SurveyQuestionDto> ChildQuestions { get; set; }
    }
}