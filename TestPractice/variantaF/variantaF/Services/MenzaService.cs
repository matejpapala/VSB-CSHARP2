using System.Xml.Linq;

namespace variantaF.Services
{

    public class MenzaService
    {
        private readonly HttpClient _httpClient;
        
        public MenzaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Models.XmlModel>> MenzaPost(string chosenDate)
        {
            string url = "https://csharp.janjanousek.cz/api/menza-xml/";
            HttpContent content = null;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "VSB");
            content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("date", chosenDate)
            });


            HttpResponseMessage response = await _httpClient.PostAsync(url, content);


            if (response.IsSuccessStatusCode)
            {
                string xmlData = await response.Content.ReadAsStringAsync();
                XDocument xdoc = XDocument.Parse(xmlData);

                var MealList = new List<Models.XmlModel>();
                var MealsFound = xdoc.Descendants().Where(x => x.Elements().Any(e => e.Name.LocalName == "mealKindId" && e.Value == "2"));

                foreach(var meal in MealsFound)
                {
                    MealList.Add(new Models.XmlModel
                    {
                        Name = meal.Elements().FirstOrDefault(x => x.Name.LocalName == "mealName")?.Value,
                        Price = meal.Elements().FirstOrDefault(x => x.Name.LocalName == "price")?.Value,
                        AltId = meal.Elements().FirstOrDefault(x => x.Name.LocalName == "altId")?.Value,
                        Date = meal.Elements().FirstOrDefault(x => x.Name.LocalName == "date")?.Value
                    });
                }

                return MealList;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                int waitTime = (int)response.Headers.RetryAfter.Delta.Value.TotalSeconds;
                await Task.Delay(waitTime);
                return await MenzaPost(chosenDate);
            }

            return new List<Models.XmlModel>();
        }
    }
}
