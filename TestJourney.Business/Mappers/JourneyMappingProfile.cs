using AutoMapper;
using TestJourney.Business.Class;
using TestJourney.Business.DTO;

namespace TestJourney.Business.Mappers
{
    public class JourneyMappingProfile : Profile
    {
        public JourneyMappingProfile() 
        {
            CreateMap<RequestJourneyDto, RequestJourney>();
            CreateMap<FlightNewshoreAir, JourneyDto>();
        }
    }
}
