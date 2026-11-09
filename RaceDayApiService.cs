using System.Net.Http;
using System.Threading.Tasks;

namespace RaceDayMVC1.Services
{
    public class RaceDayApiService
    {
        private readonly HttpClient _httpClient;

        public RaceDayApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RaceDayApi");
        }

        // Test the connection to the RaceDay API
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync("api/events");

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}