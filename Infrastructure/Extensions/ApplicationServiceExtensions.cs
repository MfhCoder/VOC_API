using Application.Services;
using Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            
            return services;
        }
    }
}