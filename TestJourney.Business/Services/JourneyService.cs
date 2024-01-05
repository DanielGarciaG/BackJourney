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

        public async Task<JourneyDto> GetCalculatedRoute(RequestJourneyDto requestJourneyDto)
        {
            RequestJourney requestJourney = _mapper.Map<RequestJourney>(requestJourneyDto);
            List<FlightNewshoreAir> FlightsNewshoreAir = await _newshoreAir.FindAssociatedFlights(requestJourney);

            //Algoritmo para calcular rutas
            List<FlightNewshoreAir> flightsCalculated = CalculateRoute(FlightsNewshoreAir, requestJourneyDto.Origin, requestJourneyDto.Destination, 5);

            //Este mapeo debe analizarse bien
            JourneyDto journey = _mapper.Map<JourneyDto>(FlightsNewshoreAir);

            return journey;
        }

        private List<FlightNewshoreAir>? CalculateRoute(List<FlightNewshoreAir> FlightsNewshoreAir, string origin, string destination, int numJourneys)
        {
            numJourneys -= 1;
            if (numJourneys == 0) 
                return null;

            List<FlightNewshoreAir> returnListFlights = new();
            List<FlightNewshoreAir> FlightsByDestination = FlightsNewshoreAir.Where(x => x.ArrivalStation.Equals(destination)).ToList();

            FlightNewshoreAir flight = FlightsByDestination.FirstOrDefault(x => x.DepartureStation.Equals(origin));
            if (flight != null)
            {
                returnListFlights.Add(flight);
                return returnListFlights;
            }               

            foreach (FlightNewshoreAir flightNewshoreAir in FlightsByDestination)
            {
                returnListFlights = CalculateRoute(FlightsNewshoreAir, origin, flightNewshoreAir.DepartureStation, numJourneys);
                if (flightNewshoreAir != null) 
                    returnListFlights.Add(flightNewshoreAir);

                if (returnListFlights.Count > 0) 
                    break;
            }

            return returnListFlights;
        }
    }
}
