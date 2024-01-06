using TestJourney.Business.DTO;

namespace TestJourney.Business.Interfaces
{
    public interface IJourneyService
    {
        public Task<ResponseJourneyDto> GetCalculatedRoute(RequestJourneyDto requestJourneyDto);
    }
}
