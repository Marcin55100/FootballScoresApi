namespace FootballScoresApi.Helpers
{
    public class HttpApiProvider : IHttpApiProvider
    {
        private HttpClient _httpClient;

        private const string API_KEY_DEF = "X-RapidAPI-Key";
        private const string API_KEY_VALUE = "0b647f6580mshe3d7d50be0da1a0p196931jsnbd32120feff2";
        private const string API_HOST_DEF = "X-RapidAPI-Host";
        private const string API_HOST_VALUE = "api-football-v1.p.rapidapi.com";

        public async Task<string?> GetResponse(string uri)
        {
            _httpClient = new HttpClient();
            using (var response = await _httpClient.SendAsync(CreateHttpMessage(uri, HttpMethod.Get)))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return body;
            }
        }

        private HttpRequestMessage CreateHttpMessage(string uri, HttpMethod httpMethod)
        {
            return new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(uri),
                Headers =
                {
                    { API_KEY_DEF, API_KEY_VALUE },
                    { API_HOST_DEF, API_HOST_VALUE },
                }
            };
        }
    }
}
