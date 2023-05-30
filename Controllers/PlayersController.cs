using FootballScoresApi.Api;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<StandingsController> _logger;
        private readonly IScoresApiProvider _scoresApiProvider;

        public PlayersController(ILogger<StandingsController> logger, IScoresApiProvider scoresApiProvider)
        {
            _logger = logger;
            _scoresApiProvider = scoresApiProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFromTeam(string teamName)
        {
            return Ok(await _scoresApiProvider.GetAllPlayers(teamName));
        }
    }
}
