using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestJourney.Business.Class;
using TestJourney.Business.Interfaces;
using TestJourney.Business.Services;

namespace TestJourney.DataAccess.Integrations
{
    public class NewshoreAir : INewshoreAir
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<JourneyService> _logger;

        public NewshoreAir(ApplicationContext context, ILogger<JourneyService> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<FlightNewshoreAir>> FindAssociatedFlights(RequestJourney requestJourney)
        {
            try
            {
                _logger.LogInformation("Enter FindAssociatedFlights");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_context.UrlApiNewshoreAir);

                List<FlightNewshoreAir> flightsNewshoreAir = new();
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successful response");
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    flightsNewshoreAir = JsonConvert.DeserializeObject<List<FlightNewshoreAir>>(jsonResponse);
                }

                return flightsNewshoreAir;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while querying the NEWSHORE AIR API: {ex.GetBaseException()}");
                throw;
            }
        }
    }
}
