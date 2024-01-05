using FootballScoresApi.Api;
using FootballScoresApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixturesController : ControllerBase
    {
        private readonly ILogger<FixturesController> _logger;
        private readonly IScoresApiProvider _scoresApiProvider;

        public FixturesController(ILogger<FixturesController> logger, IScoresApiProvider scoresApiProvider)
        {
            _logger = logger;
            _scoresApiProvider = scoresApiProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetFixtureByDate(string teamName, DateTime dateTime)
        {
            var fixtures = await _scoresApiProvider.TryGetFixtureByDate(teamName, dateTime);
            if (fixtures == null)
            {
                return NotFound();
            }
            return Ok(fixtures);
        }

        [HttpGet]
        [Route("last")]
        public async Task<IActionResult> GetFixtureByDate(string teamName, int numberOfMatches)
        {
            var fixtures = await _scoresApiProvider.TryGetLastFixtures(teamName, numberOfMatches);
            if (fixtures == null)
            {
                return NotFound();
            }
            return Ok(fixtures);
        }
    }
}
