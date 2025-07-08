using FootballScoresApi.Api;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly ILogger<StandingsController> _logger;
        private readonly IScoresApiProvider _scoresApiProvider;

        public StandingsController(ILogger<StandingsController> logger, IScoresApiProvider scoresApiProvider)
        {
            _logger = logger;
            _scoresApiProvider = scoresApiProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int season)
        {
            return Ok(await _scoresApiProvider.GetAllStandings(season));
        }
    }
}
