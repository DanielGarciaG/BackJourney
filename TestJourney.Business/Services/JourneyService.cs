using AutoMapper;
using TestJourney.Business.Class;
using TestJourney.Business.DTO;
using TestJourney.Business.Interfaces;
using Microsoft.Extensions.Logging;

namespace TestJourney.Business.Services
{
    public class JourneyService : IJourneyService
    {
        public readonly INewshoreAir _newshoreAir;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;
        private readonly ILogger<JourneyService> _logger;

        public JourneyService(INewshoreAir newshoreAir, IMapper mapper, ApplicationContext context, ILogger<JourneyService> logger) 
        {
            _newshoreAir = newshoreAir;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseJourneyDto> GetCalculatedRoute(RequestJourneyDto requestJourneyDto)
        {
            try
            {
                _logger.LogInformation("Enter GetCalculatedRoute");
                _logger.LogInformation($"Origin: {requestJourneyDto.Origin} Destination: {requestJourneyDto.Destination}");
                RequestJourney requestJourney = _mapper.Map<RequestJourney>(requestJourneyDto);
                List<FlightNewshoreAir> FlightsNewshoreAir = await _newshoreAir.FindAssociatedFlights(requestJourney);

                List<FlightNewshoreAir> flightsCalculated = CalculateRoute(FlightsNewshoreAir, requestJourneyDto.Origin, requestJourneyDto.Destination, _context.MaximumNumberFlights);

                if (flightsCalculated.Count == 0)
                    return null;

                List<FlightDto> flightDto = _mapper.Map<List<FlightDto>>(flightsCalculated);

                ResponseJourneyDto journeyDto = new();
                journeyDto.Journey = new();
                journeyDto.Journey.Origin = requestJourneyDto.Origin;
                journeyDto.Journey.Destination = requestJourneyDto.Destination;
                journeyDto.Journey.Flight = flightDto;
                journeyDto.Journey.CalculateTotalPrice();

                return journeyDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error has occurred in the route calculation process: {ex.GetBaseException()}");
                throw;
            }
            
        }

        private List<FlightNewshoreAir>? CalculateRoute(List<FlightNewshoreAir> FlightsNewshoreAir, string origin, string destination, int numJourneys)
        {
            try
            {
                _logger.LogInformation($"Enter CalculateRoute num journey: {numJourneys}");
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
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while calculating routes { ex.GetBaseException()}");
                throw;
            }
            
        }
    }
}
