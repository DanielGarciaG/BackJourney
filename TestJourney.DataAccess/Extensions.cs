using Microsoft.Extensions.DependencyInjection;
using TestJourney.Business.Interfaces;
using TestJourney.DataAccess.Integrations;

namespace TestJourney.DataAccess
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<INewshoreAir, NewshoreAir>();

            return services;
        }
    }
}
