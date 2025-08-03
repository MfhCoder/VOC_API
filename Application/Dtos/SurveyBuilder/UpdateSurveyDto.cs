using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.SurveyBuilder
{
    public class GetSurveyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? Connected_DB { get; set; }
        public bool? Status { get; set; }
        public string? MessageContent { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetQuestionSectionDto> Sections { get; set; }
    }

    public class GetQuestionSectionDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int SectionOrder { get; set; }
        public List<GetSurveyQuestionDto> Questions { get; set; }
    }
    public class GetSurveyQuestionDto
    {
        public int? Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestionOrder { get; set; }
        public bool IsRequired { get; set; }
        public int QuestionTypeId { get; set; }
        public int? TriggerOptionId { get; set; }

        public List<GetQuestionOptionDto>? Options { get; set; }
        //public List<QuestionBranchDto> ChildBranches { get; set; }
    }

    public class GetQuestionOptionDto
    {
        public int? Id { get; set; }
        public string OptionText { get; set; }
        public int OptionOrder { get; set; }
    }

    public class GetQuestionBranchDto
    {
        public int TriggerOptionId { get; set; }
        public List<GetSurveyQuestionDto> ChildQuestions { get; set; }
    }
}