using FootballScoresApi.Api.Model;
using FootballScoresApi.Helpers;
using FootballScoresApi.Model;
using Newtonsoft.Json;

namespace FootballScoresApi.Api
{
    public class ScoresApiProvider : IScoresApiProvider
    {
        private const string ALL_TEAMS_ENDP = "https://api-football-v1.p.rapidapi.com/v3/teams?league=39&season=2020";
        private const string GET_ID_ENDP = $"https://api-football-v1.p.rapidapi.com/v3/teams?country={LEAGUE_ORIGIN}&name=";
        private const string STANDINGS_ENDP = "https://api-football-v1.p.rapidapi.com/v3/standings?season=2022&league=39";
        private const string FIXTURE_FOR_DATE_ENDP = "https://api-football-v1.p.rapidapi.com/v3/fixtures?season=2022&league=39";
        private const string LEAGUE_ORIGIN = "England";
        private readonly IHttpApiProvider _httpApiProvider;
        private readonly ILogger<ScoresApiProvider> _logger;

        public ScoresApiProvider(IHttpApiProvider httpApiProvider, ILogger<ScoresApiProvider> logger)
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

        public async Task<Fixture> TryGetFixtureByDate(string team, DateTime dateTime)
        {
            var parsedData = dateTime.ToString("yyyy-MM-dd");
            var teamId = await GetTeamId(team);
            if (teamId == null)
            {
                throw new KeyNotFoundException();
            }

            var response = await _httpApiProvider.GetResponse($"{FIXTURE_FOR_DATE_ENDP}&team={teamId}&date={parsedData}");
            var fixtures = JsonConvert.DeserializeObject<Fixtures>(response);

            var teams = fixtures?.response?.FirstOrDefault()?.teams;
            if (teams != null)
            {
                var isHome = teams.home.id == teamId;
                return new Fixture(isHome, isHome ? teams.away.name : teams.home.name);
            }
            return null;
        }


        public async Task<List<TeamData>> GetAllStandings()
        {
            var list = new List<TeamData>();
            try
            {
                var response = await _httpApiProvider.GetResponse(STANDINGS_ENDP);
                var teamsList = JsonConvert.DeserializeObject<Standings>(response);

                var league = teamsList?.response?.ToList().FirstOrDefault()?.league;

                league?.standings?
                    .FirstOrDefault()?
                    .ToList()
                    .ForEach(s => list.Add(new TeamData(s.team.name, s.rank, s.points)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
            }
            return list;
        }
    }
}
