using AutoMapper;
using TestJourney.Business.Class;
using TestJourney.Business.DTO;
using TestJourney.Business.Interfaces;

namespace TestJourney.Business.Services
{
    public class JourneyService : IJourneyService
    {
        public readonly INewshoreAir _newshoreAir;
        private readonly IMapper _mapper;

        public JourneyService(INewshoreAir newshoreAir, IMapper mapper) 
        {
            _newshoreAir = newshoreAir;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<JourneyDto> GetAssociatedFlights(RequestJourneyDto requestJourneyDto)
        {
            RequestJourney requestJourney = _mapper.Map<RequestJourney>(requestJourneyDto);
            List<FlightNewshoreAir> FlightsNewshoreAir = await _newshoreAir.FindAssociatedFlights(requestJourney);

            //Este mapeo debe analizarse bien
            JourneyDto journey = _mapper.Map<JourneyDto>(FlightsNewshoreAir);

            return journey;
        }
    }
}
