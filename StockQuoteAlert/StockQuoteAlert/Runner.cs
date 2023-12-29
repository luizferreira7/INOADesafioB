using System.Text.Json;
using StockQuoteAlert.Business;
using StockQuoteAlert.Constants;
using StockQuoteAlert.Model.Validators;
using StockQuoteAlert.Model;
using StockQuoteAlert.Utility;

namespace StockQuoteAlert;

public class Runner
{
    public Runner()
    {
    }
    
    public void Start(string[] args)
    {
        var parser = new Parser();
        
        var arguments = parser.ParseArgs(args);

        var argumentsValidator = new ArgumentsValidator(arguments);
        
        argumentsValidator.Validate();

        var envLoader = new DotEnvLoader();

        var apiUrl = envLoader.GetEnvByKey(EnvironmentVariables.API_URL);
        var apiPath = envLoader.GetEnvByKey(EnvironmentVariables.API_STOCK_PATH);

        var paramMap = new Dictionary<string, string>();

        if (arguments.stock is not null)
        {
            paramMap.Add(Params.STOCK_PARAM, arguments.stock);
        
            var resquestHandler = new ResquestHandler();

            
            var jsonString = resquestHandler.MakeRequest(apiUrl, apiPath, paramMap).Result;

            if (jsonString is not null)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
        
                var stock = JsonSerializer.Deserialize<StockPriceDTO>(jsonString, options);
        
                Console.WriteLine(stock);
            }
            
        }
    }
}