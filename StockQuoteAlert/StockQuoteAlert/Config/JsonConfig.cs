using System.Text.Json;

namespace StockQuoteAlert.Config;

public class JsonConfig
{
    public JsonSerializerOptions _caseInsensitiveSerializerSettings { get; set; }

    public JsonConfig()
    {
        _caseInsensitiveSerializerSettings = new JsonSerializerOptions();
        _caseInsensitiveSerializerSettings.PropertyNameCaseInsensitive = true;
    }
}