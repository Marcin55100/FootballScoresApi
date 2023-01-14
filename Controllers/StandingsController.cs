using FootballScoresApi.Api;
using FootballScoresApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<List<TeamData>> GetAll()
        {
            return await _scoresApiProvider.GetAllStandings();
        }
    }
}
