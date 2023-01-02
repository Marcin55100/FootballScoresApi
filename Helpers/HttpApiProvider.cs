namespace FootballScoresApi.Helpers
{
    public class HttpApiProvider : IHttpApiProvider
    {
        private static readonly HttpRequestMessage _httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = null,
            Headers =
                {
                    { "X-RapidAPI-Key", "0b647f6580mshe3d7d50be0da1a0p196931jsnbd32120feff2" },
                    { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                },
        };

        public async Task<string?> GetResponse(string uri)
        {
            var client = new HttpClient();
            _httpRequestMessage.RequestUri = new Uri(uri);

            using (var response = await client.SendAsync(_httpRequestMessage))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return body;
            }
        }
    }
}
