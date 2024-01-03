using FootballScoresApi.Model;

namespace FootballScoresApi.Api
{
    public interface IScoresApiProvider
    {
        Task<List<TeamData>> GetAllStandings();
        Task<Fixture> TryGetFixtureByDate(string team, DateTime dateTime);
        Task<List<Fixture>> TryGetLastFixtures(string team, int numberOfMatches);
    }
}