using ASVSSECURITYAUDITOR.Core.Interfaces;
using ASVSSECURITYAUDITOR.Core.Services;

namespace ASVSSECURITYAUDITOR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ICsvImportService, CsvImportService>();
            services.AddScoped<IAIExplanationService,OpenAIExplanationService>();

            return services;
        }
        
    }
}
