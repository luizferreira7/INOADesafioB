using System.Text.Json;
using StockQuoteAlert.Exception;
using StockQuoteAlert.Model;

namespace StockQuoteAlert.Business;
using System;

public class ResquestHandler
{
    public ResquestHandler()
    {
    }

    public async Task<string?> MakeRequest(string apiUrl, string path, Dictionary<string, string> paramMap)
    {
        var client = new HttpClient();

        try
        {
            var url = apiUrl + path + BuildQueryParam(paramMap);
            
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API response: {jsonString}");
                return jsonString;
            }
            else
            {
                throw new RequestErrorException(response.StatusCode.ToString());
            }
        }
        catch (Exception e)
        {
            throw new RequestErrorException(e);
        }
    }

    protected string BuildQueryParam(Dictionary<string, string> paramMap)
    {
        if (!paramMap.Any())
        {
            return string.Empty;
        }

        var queryString = string.Join("&", paramMap.Select(param => $"{param.Key}={param.Value}"));

        return "?" + queryString;
    }
}