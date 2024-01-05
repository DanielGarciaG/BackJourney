using Newtonsoft.Json;
using TestJourney.Business.Class;
using TestJourney.Business.Interfaces;

namespace TestJourney.DataAccess.Integrations
{
    public class NewshoreAir : INewshoreAir
    {
        public async Task<List<FlightNewshoreAir>> FindAssociatedFlights(RequestJourney requestJourney)
        {
            string apiUrl = "https://recruiting-api.newshore.es/api/flights/0";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            List<FlightNewshoreAir> flightsNewshoreAir = new();
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                flightsNewshoreAir = JsonConvert.DeserializeObject<List<FlightNewshoreAir>>(jsonResponse);
            }

            return flightsNewshoreAir;
        }
    }
}
