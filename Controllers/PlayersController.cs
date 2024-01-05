using FootballScoresApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<FixturesController> _logger;
        private readonly IPlayersService _playersService;

        public PlayersController(ILogger<FixturesController> logger, IPlayersService playersService)
        {
            _logger = logger;
            _playersService = playersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string teamName)
        {
            return Ok(await _playersService.GetAll(teamName));
        }
    }
}
