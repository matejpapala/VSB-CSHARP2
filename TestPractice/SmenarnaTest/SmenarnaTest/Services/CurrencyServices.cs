using SmenarnaTest.Models;
using System.Text.Json;

namespace SmenarnaTest.Services
{
    public class CurrencyServices
    {
        private readonly HttpClient _httpClient;

        public CurrencyServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRateResponse> GetCourses()
        {
            string url = "https://data.kurzy.cz/json/meny/b%5B1%5D.json";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string jsonText = await response.Content.ReadAsStringAsync();

            var kurzy = JsonSerializer.Deserialize<ExchangeRateResponse>(jsonText);

            return kurzy;
        }
    }
}
