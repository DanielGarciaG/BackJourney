using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{origin}/{destination}")]
        public async Task<IActionResult> Post(string origin, string destination)
        {
            try
            {
                RequestJourneyDto requestJourneyDto = new RequestJourneyDto
                {
                    Origin = origin,
                    Destination = destination
                };

                ResponseJourneyDto journey = await _journeyService.GetCalculatedRoute(requestJourneyDto);

                if (journey == null)
                    return BadRequest( new { message = "Su solicitud no pudo ser procesada" });

                return Ok(journey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
        }
    }
}
