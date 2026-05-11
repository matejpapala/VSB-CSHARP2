using System.Text.Json.Serialization;

namespace SmenarnaTest.Models
{

    public class CurrencyInfo
    {
        [JsonPropertyName("dev_stred")]
        public double DevStred { get; set; }
    }
    public class ExchangeRateResponse
    {
        [JsonPropertyName("kurzy")]
        public Dictionary<string, CurrencyInfo> Kurzy { get; set; }
    }
}
