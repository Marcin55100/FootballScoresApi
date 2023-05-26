using FootballScoresApi.Api;
using FootballScoresApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _scoresApiProvider.GetAllStandings());
        }

        [HttpGet]
        [Route("fixtures")]
        public async Task<IActionResult> GetFixtureByDate(string team, DateTime dateTime)
        {
            var fixtures = await _scoresApiProvider.TryGetFixtureByDate(team, dateTime);
            if (fixtures == null)
            {
                return NotFound();
            }
            return Ok(fixtures);
        }
    }
}
