using System.Text.Json;
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
                Console.WriteLine($"Resposta da API: {jsonString}");
                return jsonString;
            }
            else
            {
                Console.WriteLine($"Erro na requisição. Status Code: {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro na requisição: {e.Message}");
        }

        return null;
    }

    private string BuildQueryParam(Dictionary<string, string> paramMap)
    {
        if (!paramMap.Any())
        {
            return string.Empty;
        }

        var queryString = string.Join("&", paramMap.Select(param => $"{param.Key}={param.Value}"));

        return "?" + queryString;
    }
}