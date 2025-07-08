using AutoMapper;
using FootballScoresApi.Api.Model;
using FootballScoresApi.Consts;
using FootballScoresApi.Helpers;
using FootballScoresApi.Model;
using FootballScoresApi.Services;
using Newtonsoft.Json;

namespace FootballScoresApi.Api
{
    public class ScoresApiProvider : IScoresApiProvider
    {
        private const string STANDINGS_ENDP = $"{GlobalConsts.BASIC_URL}/standings?league=39";
        private const string FIXTURE_FOR_DATE_ENDP = $"{GlobalConsts.BASIC_URL}/fixtures?season=2024&league=39";
        private const string LAST_FIXTURES_ENDP = $"{GlobalConsts.BASIC_URL}/fixtures?league=39";

        private readonly ITeamsService _teamsService;
        private readonly IHttpApiProvider _httpApiProvider;
        private readonly ILogger<ScoresApiProvider> _logger;

        public ScoresApiProvider(IHttpApiProvider httpApiProvider, ILogger<ScoresApiProvider> logger, ITeamsService teamsService)
        {
            _logger = logger;
            _teamsService = teamsService;
            _httpApiProvider = httpApiProvider;
        }

        public async Task<Fixture> TryGetFixtureByDate(string teamName, DateTime dateTime)
        {
            var teamId = await _teamsService.GetTeamId(teamName);
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
                return new Fixture(teamName, isHome ? teams.away.name : teams.home.name, isHome, dateTime);
            }
            return null;
        }

        public async Task<List<Fixture>> TryGetLastFixtures(string teamName, int numberOfMatches, int season)
        {
            var teamId = await _teamsService.GetTeamId(teamName);
            if (teamId == null)
            {
                throw new KeyNotFoundException();
            }

            var fixtureList = new List<Fixture>();
            var response = await _httpApiProvider.GetResponse($"{LAST_FIXTURES_ENDP}&team={teamId}&last={numberOfMatches}&season={season}");
            var fixtures = JsonConvert.DeserializeObject<Fixtures>(response);

            fixtures?.response?.ToList()?.ForEach(f =>
            {
                var isHome = f.teams.home.id == teamId;
                fixtureList.Add(new Fixture(teamName, isHome ? f.teams.away.name : f.teams.home.name, isHome, f.fixture.date));
            });
            return fixtureList.Any() ? fixtureList : null;
        }


        public async Task<List<TeamData>> GetAllStandings(int season)
        {
            var list = new List<TeamData>();
            var response = await _httpApiProvider.GetResponse($"{STANDINGS_ENDP}&season={season}");
            var teamsList = JsonConvert.DeserializeObject<Standings>(response);

            var league = teamsList?.response?.ToList().FirstOrDefault()?.league;

            league?.standings?
                .FirstOrDefault()?
                .ToList()?
                .ForEach(s => list.Add(new TeamData(s.team.name, s.rank, s.points)));
            return list;
        }
    }
}
