// Application/Extensions/ApplicationServiceExtensions.cs
//using ;
//using Application.Services;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using AutoMapper.Internal;
//using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register services
            //services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<IUserService, UserService>();
            
            //// AutoMapper
            //services.AddAutoMapper(typeof(Application.Mappings.DeliveryProfile).Assembly);

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}