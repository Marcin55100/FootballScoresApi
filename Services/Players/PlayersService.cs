using AutoMapper;
using FootballScoresApi.Api.Model;
using FootballScoresApi.Consts;
using FootballScoresApi.Helpers;
using FootballScoresApi.Model;
using Newtonsoft.Json;

namespace FootballScoresApi.Services
{
    public class PlayersService : IPlayersService
    {
        private const string GET_ALL_ENDP = $"{GlobalConsts.BASIC_URL}/players/squads";

        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;
        private readonly ILogger<PlayersService> _logger;
        private readonly IHttpApiProvider _httpApiProvider;

        public PlayersService(IHttpApiProvider httpApiProvider, ILogger<PlayersService> logger, IMapper mapper, ITeamsService teamsService)
        {
            _logger = logger;
            _mapper = mapper;
            _teamsService = teamsService;
            _httpApiProvider = httpApiProvider;
        }

        public async Task<List<PlayerDto>> GetAllPlayers(string teamName)
        {
            var teamId = await _teamsService.GetTeamId(teamName);
            if (teamId == null)
            {
                throw new KeyNotFoundException();
            }

            var response = await _httpApiProvider.GetResponse($"{GET_ALL_ENDP}?team={teamId}");
            var squads = JsonConvert.DeserializeObject<Squads>(response);
            var players = squads.response?.FirstOrDefault()?.players;
            if (players?.Any() ?? false)
            {
                return players.Select(p => _mapper.Map<PlayerDto>(p)).ToList();
            }
            return null;
        }
    }
}
