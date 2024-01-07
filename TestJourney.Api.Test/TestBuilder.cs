using Microsoft.AspNetCore.Mvc.Testing;
using TestJourney.Business.DTO;

namespace TestJourney.Api.Test
{
    public abstract class IntegrationTestBuilder : IDisposable
    {
        protected HttpClient TestClient;
        private bool Disposed;

        protected IntegrationTestBuilder()
        {
            BootstrapTestingSuite();
        }

        protected void BootstrapTestingSuite()
        {
            Disposed = false;
            var appFactory = new WebApplicationFactory<TestJourney.Api.Controllers.JourneyController>();
            TestClient = appFactory.CreateClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                TestClient.Dispose();
            }

            Disposed = true;
        }
    }

    public class ResponseJourneyDto
    {
        public JourneyDto journey { get; set; }
    }

    public class JourneyDto
    {
        public List<FlightDto> flight { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public double price { get; set; }
        public double CalculateTotalPrice()
        {
            double totalPrice = 0;
            foreach (var flight in flight)
                totalPrice += flight.price;

            price = totalPrice;

            return totalPrice;
        }
    }

    public class FlightDto
    {
        public TransportDto transport { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public double price { get; set; }
        public string departureStation;
    }
}
