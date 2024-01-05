using FootballScoresApi.Model;

namespace FootballScoresApi.Services
{
    public interface IPlayersService
    {
        Task<List<PlayerDto>> GetAll(string teamName);
    }
}