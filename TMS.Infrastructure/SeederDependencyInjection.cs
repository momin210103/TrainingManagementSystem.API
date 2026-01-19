using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Infrastructure.Persistence.Data.Seed;

namespace TMS.Infrastructure
{
    public static class SeederDependencyInjection
    {
        public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddScoped<IDataSeeder, TrainingCategorySeeder>();
          services.AddScoped<IDataSeeder,TrainingPlanSeeder>();
            services.AddScoped<IDataSeeder,TrainingClassSeeder>();



            services.AddScoped<MasterSeeder>();
            return services;
        }
    }
}
