namespace FootballScoresApi.Helpers
{
    public interface IHttpApiProvider
    {
        Task<string?> GetResponse(string uri);
    }
}