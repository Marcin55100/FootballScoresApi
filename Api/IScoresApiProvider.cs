using FootballScoresApi.Model;

namespace FootballScoresApi.Api
{
    public interface IScoresApiProvider
    {
        Task<List<TeamData>> GetAllStandings();
        Task<List<TeamData>> GetAllTeams();
        List<TeamData> GetFakeTeams();
    }
}