using FootballScoresApi.Model;

namespace FootballScoresApi.Api
{
    public interface IScoresApiProvider
    {
        Task<List<Team>> GetAllTeams();
        List<Team> GetFakeTeams();
    }
}