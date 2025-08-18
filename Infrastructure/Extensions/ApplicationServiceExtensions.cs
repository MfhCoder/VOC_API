using Application.Interfaces;
using Application.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IEmailService, EmailService>();

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection"))); 
            services.AddHangfireServer();

            // Hangfire setup...
            services.AddScoped<IBackgroundJobManager, BackgroundJobManager>();
            services.AddScoped<ICustomerFeedbackRpo, CustomerFeedbackRpo>();

            return services;
        }
    }
}