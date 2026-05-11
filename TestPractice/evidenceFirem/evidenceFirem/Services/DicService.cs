using evidenceFirem.Models;
using System.Text.Json;

namespace evidenceFirem.Services
{
    public class DicService
    {
        private readonly HttpClient _httpClient;

        public DicService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JsonModel> ValidateDic(string Dic)
        {
            string url = @"https://csharp.janjanousek.cz/api/dic/";
            //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "VSB");
            _httpClient.DefaultRequestHeaders.Remove("X-AppName");
            _httpClient.DefaultRequestHeaders.Add("X-AppName", "vsb");
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("dic", Dic)
            });

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<JsonModel>(json);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(5000);
                return await ValidateDic(Dic);
            }
            return new JsonModel();
        }
    }
}
