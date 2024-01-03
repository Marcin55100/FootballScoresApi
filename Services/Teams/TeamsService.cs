using AutoMapper;
using FootballScoresApi.Api.Model;
using FootballScoresApi.Consts;
using FootballScoresApi.Helpers;
using FootballScoresApi.Model;
using Newtonsoft.Json;

namespace FootballScoresApi.Services
{
    public class TeamsService : ITeamsService
    {
        private const string ALL_TEAMS_ENDP = $"{GlobalConsts.BASIC_URL}/teams?league=39&season=2020";
        private const string GET_ID_ENDP = $"{GlobalConsts.BASIC_URL}/teams?country={GlobalConsts.LEAGUE_ORIGIN}&name=";

        private readonly ILogger<TeamsService> _logger;
        private readonly IHttpApiProvider _httpApiProvider;

        public TeamsService(IHttpApiProvider httpApiProvider, ILogger<TeamsService> logger)
        {
            _logger = logger;
            _httpApiProvider = httpApiProvider;
        }

        public List<TeamData> GetFakeTeams()
        {
            return new List<TeamData>()
            {
                new TeamData("Arsenal", 1, 30),
                new TeamData("Liverpool", 2, 20),
                new TeamData("Manchester United", 3, 10)
            };
        }

        public async Task<List<TeamData>> GetAllTeams()
        {
            var list = new List<TeamData>();
            var response = await _httpApiProvider.GetResponse(ALL_TEAMS_ENDP);
            var teamsList = JsonConvert.DeserializeObject<TeamsList>(response);

            teamsList?.response?
                .ToList()?
                .ForEach(t => list.Add(new TeamData(t.team.name, t.team.id)));
            return list;
        }

        public async Task<int?> GetTeamId(string name)
        {
            var response = await _httpApiProvider.GetResponse($"{GET_ID_ENDP}{name}");
            var teamsList = JsonConvert.DeserializeObject<TeamsList>(response);
            return teamsList?.response?.ToList()?.FirstOrDefault()?.team?.id;
        }
    }
}
