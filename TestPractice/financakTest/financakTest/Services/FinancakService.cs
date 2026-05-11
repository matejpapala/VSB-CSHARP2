using financakTest.Models;
using System.Xml.Linq;

namespace financakTest.Services
{
    public class FinancakService
    {
        private readonly HttpClient _httpClient;

        public FinancakService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FInancakModel>> GetXml()
        {
            string url = "https://apl2.czso.cz/iSMS/do_cis_export?kodcis=46&typdat=0&cisjaz=203&format=0";

            string response = await _httpClient.GetStringAsync(url);

            XDocument xdoc = XDocument.Parse(response);

            List<FInancakModel> financakList = new List<FInancakModel>();

            var polozky = xdoc.Descendants().Where(x => x.Name.LocalName == "POLOZKA");

            foreach(var item in polozky)
            {
                string chodnota = item.Elements().FirstOrDefault(e => e.Name.LocalName == "CHODNOTA")?.Value;
                string text = item.Elements().FirstOrDefault(e => e.Name.LocalName == "TEXT")?.Value;

                if(chodnota != null && text != null)
                {
                    financakList.Add(new FInancakModel
                    {
                        chodnota = chodnota,
                        text = text
                    });
                }
            }

            return financakList;
        }
    }
}
