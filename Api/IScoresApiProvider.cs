using FootballScoresApi.Model;

namespace FootballScoresApi.Api
{
    public interface IScoresApiProvider
    {
        Task<List<PlayerDto>> GetAllPlayers(string teamName);
        Task<List<TeamData>> GetAllStandings();
        Task<List<TeamData>> GetAllTeams();
        List<TeamData> GetFakeTeams();
        Task<Fixture> TryGetFixtureByDate(string team, DateTime dateTime);
        Task<List<Fixture>> TryGetLastFixtures(string team, int numberOfMatches);
    }
}