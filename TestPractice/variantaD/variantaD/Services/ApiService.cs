using System.Xml.Linq;
using variantaD.Models;

namespace variantaD.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetXmlApi(string psc)
        {
            string url = @"https://csharp.janjanousek.cz/api/osm-xml/";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "VSB");
            var DataToSend = new { postalcode = psc };
            var response = await _httpClient.PostAsJsonAsync(url, DataToSend);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            string xmlData = await response.Content.ReadAsStringAsync();
            Console.WriteLine(xmlData);
            XDocument xdoc = XDocument.Parse(xmlData);
            string municipality = xdoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "county")?.Value;
            return municipality;
        }

        public void SaveToFile(object model)
        {

            var modelType = model.GetType();
            var modelProps = modelType.GetProperties();

            var psc = modelType.GetProperty("Psc")?.GetValue(model)?.ToString();
            var phone = modelType.GetProperty("PhoneNumber").GetValue(model)?.ToString();

            using (StreamWriter sw = new StreamWriter($"{psc}_{phone}.txt"))
            {
                foreach(var prop in modelProps)
                {
                    sw.Write(prop.Name);
                    sw.Write(prop.GetValue(model));
                    sw.WriteLine();
                }
            }
        }
    }
}
