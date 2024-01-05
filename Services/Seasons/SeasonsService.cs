using AutoMapper;
using FootballScoresApi.Helpers;
using FootballScoresApi.Model;

namespace FootballScoresApi.Services
{
    public class SeasonsService : ISeasonsService
    {
        private readonly ITeamsService _teamsService;
        private readonly ILogger<SeasonsService> _logger;
        private readonly IHttpApiProvider _httpApiProvider;

        public SeasonsService(IHttpApiProvider httpApiProvider, ILogger<SeasonsService> logger, ITeamsService teamsService)
        {
            _logger = logger;
            _teamsService = teamsService;
            _httpApiProvider = httpApiProvider;
        }

        public async Task<List<string>> GetAll()
        {
            return new List<string>() { "2024", "2023", "2022", "2021" };
        }
    }
}
