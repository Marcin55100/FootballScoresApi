using FootballScoresApi.Api;
using FootballScoresApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly ILogger<LeagueController> _logger;
        private readonly IScoresApiProvider _scoresApiProvider;

        public LeagueController(ILogger<LeagueController> logger, IScoresApiProvider scoresApiProvider)
        {
            _logger = logger;
            _scoresApiProvider = scoresApiProvider;
        }

        [HttpGet]
        public async Task<List<Team>> GetAllTeams()
        {
            return await _scoresApiProvider.GetAllTeams();
        }

        [HttpGet("fake")]
        public List<Team> GetFakeTeams()
        {
            _logger.LogInformation($"[{nameof(LeagueController)}] Fake teams being fetched");
            return _scoresApiProvider.GetFakeTeams();
        }
    }
}
