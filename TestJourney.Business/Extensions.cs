using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TestJourney.Business.Interfaces;
using TestJourney.Business.Mappers;
using TestJourney.Business.Services;

namespace TestJourney.Business
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddMappingJourneyServices();
            services.AddScoped<IJourneyService, JourneyService>();

            return services;
        }

        private static IServiceCollection AddMappingJourneyServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new JourneyMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
