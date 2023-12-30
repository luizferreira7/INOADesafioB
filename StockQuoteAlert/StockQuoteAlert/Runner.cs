using System.Text.Json;
using StockQuoteAlert.Business;
using StockQuoteAlert.Config;
using StockQuoteAlert.Constants;
using StockQuoteAlert.Model.Validators;
using StockQuoteAlert.Model;
using StockQuoteAlert.Utility;

namespace StockQuoteAlert;

public class Runner
{
    private JsonConfig _jsonConfig;
    
    public Runner()
    {
        _jsonConfig = new JsonConfig();
    }
    
    public async void Start(string[] args)
    {
        try
        {

            var parser = new Parser();

            var arguments = parser.ParseArgs(args);

            var argumentsValidator = new ArgumentsValidator(arguments);

            argumentsValidator.Validate();

            var envLoader = new DotEnvLoader();

            var apiUrl = envLoader.GetEnvByKey(EnvironmentVariables.API_URL);
            var apiPath = envLoader.GetEnvByKey(EnvironmentVariables.API_STOCK_PATH);

            var paramMap = new Dictionary<string, string>();

            paramMap.Add(Params.STOCK_PARAM, arguments.Stock);

            var stockPriceDto = new StockPriceDTO(arguments.Stock);
            var priceReporter = new PriceReporter(arguments);
            stockPriceDto.Attach(priceReporter);

            while (true)
            {
                await Routine(stockPriceDto, apiUrl, apiPath, paramMap);
                Thread.Sleep(1000 * 60);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
    }

    private async Task Routine(StockPriceDTO stockPriceDto, string apiUrl, string apiPath, Dictionary<string, string> paramMap)
    {
        var resquestHandler = new ResquestHandler();

        var jsonString = await resquestHandler.MakeRequest(apiUrl, apiPath, paramMap);
    
        var stock = JsonSerializer.Deserialize<StockPriceDTO>(jsonString, _jsonConfig._caseInsensitiveSerializerSettings) 
                    ?? throw new InvalidOperationException("Value cannot be null");
    
        stockPriceDto.Price = stock.Price;
        stockPriceDto.Notify();
    }
}