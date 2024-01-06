using System.Diagnostics;

namespace TestJourney.Business.DTO
{
    public class JourneyDto
    {
        public List<FlightDto> Flight { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public double CalculateTotalPrice()
        {
            double totalPrice = 0;
            foreach (var flight in Flight)
                totalPrice += flight.Price;

            Price = totalPrice;

            return totalPrice;
        }
    }
}
