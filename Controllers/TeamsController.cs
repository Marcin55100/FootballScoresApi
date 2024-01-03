using FootballScoresApi.Api;
using FootballScoresApi.Model;
using FootballScoresApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamsService _teamsService;

        public TeamsController(ILogger<TeamsController> logger, ITeamsService teamsService)
        {
            _logger = logger;
            _teamsService = teamsService;
        }

        [HttpGet]
        public async Task<List<TeamData>> GetAll()
        {
            return await _teamsService.GetAllTeams();
        }

        [HttpGet("fake")]
        public List<TeamData> GetFake()
        {
            _logger.LogInformation($"[{nameof(TeamsController)}] Fake teams being fetched");
            return _teamsService.GetFakeTeams();
        }
    }
}
