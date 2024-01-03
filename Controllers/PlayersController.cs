using FootballScoresApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<StandingsController> _logger;
        private readonly IPlayersService _playersService;

        public PlayersController(ILogger<StandingsController> logger, IPlayersService playersService)
        {
            _logger = logger;
            _playersService = playersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFromTeam(string teamName)
        {
            return Ok(await _playersService.GetAllPlayers(teamName));
        }
    }
}
