using AutoMapper;
using FootballScoresApi.Api.Model;
using FootballScoresApi.Helpers;
using FootballScoresApi.Model;
using Newtonsoft.Json;

namespace FootballScoresApi.Api
{
    public class ScoresApiProvider : IScoresApiProvider
    {
        private const string BASIC_URL = "https://api-football-v1.p.rapidapi.com/v3";
        private const string ALL_TEAMS_ENDP = $"{BASIC_URL}/teams?league=39&season=2020";
        private const string GET_ID_ENDP = $"{BASIC_URL}/teams?country={LEAGUE_ORIGIN}&name=";
        private const string STANDINGS_ENDP = $"{BASIC_URL}/standings?season=2023&league=39";
        private const string FIXTURE_FOR_DATE_ENDP = $"{BASIC_URL}/fixtures?season=2023&league=39";
        private const string LAST_FIXTURES_ENDP = $"{BASIC_URL}/fixtures?league=39";
        private const string PLAYERS_ENDP = $"{BASIC_URL}/players/squads";
        private const string LEAGUE_ORIGIN = "England";

        private readonly IHttpApiProvider _httpApiProvider;
        private readonly ILogger<ScoresApiProvider> _logger;
        private readonly IMapper _mapper;

        public ScoresApiProvider(IHttpApiProvider httpApiProvider, ILogger<ScoresApiProvider> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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

        private async Task<int?> GetTeamId(string name)
        {
            var response = await _httpApiProvider.GetResponse($"{GET_ID_ENDP}{name}");
            var teamsList = JsonConvert.DeserializeObject<TeamsList>(response);
            return teamsList?.response?.ToList()?.FirstOrDefault()?.team?.id;
        }

        public async Task<Fixture> TryGetFixtureByDate(string team, DateTime dateTime)
        {
            var teamId = await GetTeamId(team);
            if (teamId == null)
            {
                throw new KeyNotFoundException();
            }

            var parsedData = dateTime.ToString("yyyy-MM-dd");
            var response = await _httpApiProvider.GetResponse($"{FIXTURE_FOR_DATE_ENDP}&team={teamId}&date={parsedData}");
            var fixtures = JsonConvert.DeserializeObject<Fixtures>(response);

            var teams = fixtures?.response?.FirstOrDefault()?.teams;
            if (teams != null)
            {
                var isHome = teams.home.id == teamId;
                return new Fixture(team, isHome ? teams.away.name : teams.home.name, isHome, dateTime);
            }
            return null;
        }

        public async Task<List<Fixture>> TryGetLastFixtures(string team, int numberOfMatches)
        {
            var teamId = await GetTeamId(team);
            if (teamId == null)
            {
                throw new KeyNotFoundException();
            }

            var fixtureList = new List<Fixture>();
            var response = await _httpApiProvider.GetResponse($"{LAST_FIXTURES_ENDP}&team={teamId}&last={numberOfMatches}");
            var fixtures = JsonConvert.DeserializeObject<Fixtures>(response);

            fixtures?.response?.ToList()?.ForEach(f =>
            {
                var isHome = f.teams.home.id == teamId;
                fixtureList.Add(new Fixture(team, isHome ? f.teams.away.name : f.teams.home.name, isHome, f.fixture.date));
            });
            return fixtureList.Any() ? fixtureList : null;
        }


        public async Task<List<TeamData>> GetAllStandings()
        {
            var list = new List<TeamData>();
            var response = await _httpApiProvider.GetResponse(STANDINGS_ENDP);
            var teamsList = JsonConvert.DeserializeObject<Standings>(response);

            var league = teamsList?.response?.ToList().FirstOrDefault()?.league;

            league?.standings?
                .FirstOrDefault()?
                .ToList()?
                .ForEach(s => list.Add(new TeamData(s.team.name, s.rank, s.points)));
            return list;
        }

        public async Task<List<PlayerDto>> GetAllPlayers(string teamName)
        {
            var response = await _httpApiProvider.GetResponse($"{PLAYERS_ENDP}?team=42");
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
