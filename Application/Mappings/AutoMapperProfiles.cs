using Application.Dtos.Merchant;
using Application.Dtos.RoleDtos;
using Application.Dtos.SurveyBuilder;
using Application.Dtos.UserDtos;
using Application.DTOs.SurveyBuilder;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Merchant mappings
            CreateMap<Merchant, MerchantDto>();

            // User mappings
            CreateMap<User, UserDto>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
              .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name)).ReverseMap();

            CreateMap<CreateUserDto, User>()
             .ForMember(dest => dest.JoiningDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateUserDto, User>();

            // Role mappings
            CreateMap<Role, RolesDto>();

            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));

            CreateMap<CreateRoleDto, Role>()
             .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            // Permission mappings
            CreateMap<CreatePermissionDto, Permission>();

            CreateMap<Permission, PermissionDto>()
                .ForMember(dest => dest.ModuleName, o => o.MapFrom(s => s.Module != null ? s.Module.Name : null))
                .ForMember(dest => dest.SurveyName, o => o.MapFrom(s => s.Survey != null ? s.Survey.Name : null));

            CreateMap<Modules, ModulesDto>();


            //Survey Builder
            //Create
            CreateMap<CreateSurveyDto, Survey>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<QuestionSectionDto, QuestionSection>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<SurveyQuestionDto, SurveyQuestion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<QuestionOptionDto, QuestionOption>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            //update
            CreateMap<Survey, GetSurveyDto>()
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.QuestionSections));
            CreateMap<QuestionSection, GetQuestionSectionDto>();
            CreateMap<SurveyQuestion, GetSurveyQuestionDto>();
            CreateMap<QuestionOption, GetQuestionOptionDto>();

            //Filters
            CreateMap<SurveyFiltersDto, SurveyFilters>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products != null ? string.Join(",", src.Products) : null))
            .ForMember(dest => dest.Ledgers, opt => opt.MapFrom(src => src.Ledgers != null ? string.Join(",", src.Ledgers) : null));

            CreateMap<SurveyFilters, SurveyFiltersDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products != null ? src.Products.Split(new char[] { ',' }).ToList() : new List<string>()))
                .ForMember(dest => dest.Ledgers, opt => opt.MapFrom(src => src.Ledgers != null ? src.Ledgers.Split(new char[] { ',' }).ToList() : new List<string>()));

            CreateMap<SurveySettingsDto, Survey>();

            CreateMap<QuestionType, QuestionTypeDto>();

        }
    }
}
