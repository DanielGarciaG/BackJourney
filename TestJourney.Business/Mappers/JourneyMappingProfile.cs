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
            CreateMap<FlightNewshoreAir, FlightDto>()
                .ForMember(x => x.Origin, m => m.MapFrom(y => y.DepartureStation))
                .ForMember(x => x.Destination, m => m.MapFrom(y => y.ArrivalStation))
                .ForPath(x => x.Transport.FlightCarrier, m => m.MapFrom(y => y.FlightCarrier))
                .ForPath(x => x.Transport.FlightNumber, m => m.MapFrom(y => y.FlightNumber))
                .ForMember(x => x.Price, m => m.MapFrom(y => y.Price));
        }
    }
}
