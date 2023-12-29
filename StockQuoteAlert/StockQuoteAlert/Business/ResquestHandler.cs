using StockQuoteAlert.Exception;

namespace StockQuoteAlert.Business;
using System;

public class ResquestHandler
{
    private HttpClient client;
    
    public ResquestHandler()
    {
        client = new HttpClient();
    }

    public async virtual Task<HttpResponseMessage> ExecuteRequest(string url)
    {
        try
        {
            var response = await client.GetAsync(url);
            return response;
        }
        catch (Exception e)
        {
            throw new RequestErrorException(e);
        }
    }

    public async Task<string> MakeRequest(string apiUrl, string path, Dictionary<string, string> paramMap)
    {
        var url = apiUrl + path + BuildQueryParam(paramMap);
            
        var response = ExecuteRequest(url).Result;
        
        if (!response.IsSuccessStatusCode)
        {
            throw new RequestErrorException((int) response.StatusCode);
        }
        
        var jsonString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"API response: {jsonString}");
        return jsonString;
    }

    public string BuildQueryParam(Dictionary<string, string> paramMap)
    {
        if (!paramMap.Any())
        {
            return string.Empty;
        }

        var queryString = string.Join("&", paramMap.Select(param => $"{param.Key}={param.Value}"));

        return "?" + queryString;
    }
}