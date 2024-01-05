namespace FootballScoresApi.Services
{
    public interface ISeasonsService
    {
        Task<List<string>> GetAll();
    }
}