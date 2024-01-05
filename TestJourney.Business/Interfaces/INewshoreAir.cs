using TestJourney.Business.Class;

namespace TestJourney.Business.Interfaces
{
    public interface INewshoreAir
    {
        Task<List<FlightNewshoreAir>> FindAssociatedFlights(RequestJourney requestJourney);
    }
}
