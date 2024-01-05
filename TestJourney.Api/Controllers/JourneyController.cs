using Microsoft.AspNetCore.Mvc;
using TestJourney.Business.Class;
using TestJourney.Business.DTO;
using TestJourney.Business.Interfaces;

namespace TestJourney.Api.Controllers
{
    [Route("api/journey")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyService _journeyService;

        public JourneyController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestJourneyDto requestJourneyDto)
        {
            JourneyDto journey = await _journeyService.GetCalculatedRoute(requestJourneyDto);
            return Ok(journey);
        }
    }
}
