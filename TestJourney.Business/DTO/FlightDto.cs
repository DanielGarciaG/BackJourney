namespace TestJourney.Business.DTO
{
    public class FlightDto
    {
        public TransportDto Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public string DepartureStation; 
    }
}
