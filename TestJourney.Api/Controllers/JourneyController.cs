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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestJourneyDto requestJourneyDto)
        {
            try
            {
                ResponseJourneyDto journey = await _journeyService.GetCalculatedRoute(requestJourneyDto);

                if (journey == null)
                    return BadRequest("Su solicitud no pudo ser procesada");

                return Ok(journey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
        }
    }
}
