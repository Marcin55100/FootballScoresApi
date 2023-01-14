using FootballScoresApi.Api;
using FootballScoresApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly IScoresApiProvider _scoresApiProvider;

        public TeamsController(ILogger<TeamsController> logger, IScoresApiProvider scoresApiProvider)
        {
            _logger = logger;
            _scoresApiProvider = scoresApiProvider;
        }

        [HttpGet]
        public async Task<List<TeamData>> GetAll()
        {
            return await _scoresApiProvider.GetAllTeams();
        }

        [HttpGet("fake")]
        public List<TeamData> GetFake()
        {
            _logger.LogInformation($"[{nameof(TeamsController)}] Fake teams being fetched");
            return _scoresApiProvider.GetFakeTeams();
        }
    }
}
