using System.Net;

namespace TestJourney.Api.Test
{
    public class JourneyControllerTest : IntegrationTestBuilder
    {
        [Fact]
        public void GetTravelWithSuccess()
        {
            string origin = "PEI";
            string destination = "MAD";

            var load = this.TestClient.GetAsync($"api/journey/{origin}/{destination}").Result;
            load.EnsureSuccessStatusCode();
            var response = load.Content.ReadAsStringAsync().Result;
            ResponseJourneyDto respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<ResponseJourneyDto>(response);
            FlightDto flightOrigin = respuestaConsulta.journey.flight.FirstOrDefault();
            FlightDto flightDestination = respuestaConsulta.journey.flight.LastOrDefault();

            Assert.Equal(flightOrigin.origin, origin);
            Assert.Equal(flightDestination.destination, destination);
        }

        [Fact]
        public void GetTravelWithError()
        {
            string origin = "IBA";
            string destination = "Bar";

            HttpResponseMessage respuesta = null;
            try
            {
                respuesta = this.TestClient.GetAsync($"api/journey/{origin}/{destination}").Result;
                respuesta.EnsureSuccessStatusCode();
                Assert.True(false, "Should failed");
            }
            catch (Exception)
            {
                Assert.Equal(respuesta.StatusCode, HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public void GetTotalPriceWithSuccess()
        {
            string origin = "PEI";
            string destination = "MAD";

            var load = this.TestClient.GetAsync($"api/journey/{origin}/{destination}").Result;
            load.EnsureSuccessStatusCode();
            var response = load.Content.ReadAsStringAsync().Result;
            ResponseJourneyDto respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<ResponseJourneyDto>(response);
            List<FlightDto> flights = respuestaConsulta.journey.flight.ToList();

            double sumTotalPrice = 0;
            foreach (FlightDto flight in flights)
            {
                sumTotalPrice += flight.price;
            }

            Assert.Equal(sumTotalPrice, respuestaConsulta.journey.CalculateTotalPrice());
        }
    }
}
