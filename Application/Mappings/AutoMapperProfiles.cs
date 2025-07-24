using Application.Dtos.Merchant;
using Application.Dtos.RoleDtos;
using Application.Dtos.UserDtos;
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

            //CreateMap<Role, RoleDto>()
            //    .ForMember(dest => dest.Permissions,
            //        opt => opt.MapFrom(src => src.RolePermissions
            //            .Select(rp => rp.Permission.Name)));

            //// Permission mappings
            //CreateMap<Permission, PermissionDto>()
            //    .ForMember(dest => dest.PermissionId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.Module.Name))
            //    .ForMember(dest => dest.SurveyName, opt => opt.MapFrom(src => src.Survey != null ? src.Survey.Title : null));

            //// Module mappings
            //CreateMap<Module, ModuleDto>()
            //    .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Id));


        }
    }
}
