using FootballScoresApi.Api.Model;
using FootballScoresApi.Helpers;
using FootballScoresApi.Model;
using Newtonsoft.Json;

namespace FootballScoresApi.Api
{
    public class ScoresApiProvider : IScoresApiProvider
    {
        private const string ALL_TEAMS_ENDP = "https://api-football-v1.p.rapidapi.com/v3/teams?league=39&season=2020";
        private readonly IHttpApiProvider _httpApiProvider;

        public ScoresApiProvider(IHttpApiProvider httpApiProvider)
        {
            _httpApiProvider = httpApiProvider;
        }

        public List<Team> GetFakeTeams()
        {
            return new List<Team>()
            {
                new Team("Arsenal", 1),
                new Team("Liverpool", 2),
                new Team("Manchester United", 3)
            };
        }

        public async Task<List<Team>> GetAllTeams()
        {
            var list = new List<Team>();
            var response = await _httpApiProvider.GetResponse(ALL_TEAMS_ENDP);
            var teamsList = JsonConvert.DeserializeObject<TeamsList>(response);

            teamsList?.response.ToList().ForEach(t => list.Add(new Team(t.team.name, t.team.id)));

            return list;
        }
    }
}
