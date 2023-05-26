using System.Text.Json;

namespace FootballScoresApi.ErrorHandlers
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
