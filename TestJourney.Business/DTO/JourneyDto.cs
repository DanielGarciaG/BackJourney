namespace TestJourney.Business.DTO
{
    public class JourneyDto
    {
        public FlightDto Flight { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
    }
}
