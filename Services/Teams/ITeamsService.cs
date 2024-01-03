using FootballScoresApi.Model;

namespace FootballScoresApi.Services
{
    public interface ITeamsService
    {
        Task<List<TeamData>> GetAllTeams();
        List<TeamData> GetFakeTeams();
        Task<int?> GetTeamId(string name);
    }
}