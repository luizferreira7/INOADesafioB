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

            var smtpValidator = new SMTPValidator();
            smtpValidator.Validate();
            
            var dotEnvValidator = new DotEnvValidator();
            dotEnvValidator.Validate();

            var envLoader = new DotEnvLoader();

            var apiUrl = envLoader.GetEnvByKey(EnvironmentVariables.API_URL);
            var apiPath = envLoader.GetEnvByKey(EnvironmentVariables.API_STOCK_PATH);
            
            var paramMap = new Dictionary<string, string>();
            
            var argumentsValidator = new ArgumentsValidator(arguments, apiUrl, apiPath);
            argumentsValidator.Validate();

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
        var requestHandler = new RequestHandler();

        var jsonString = await requestHandler.MakeRequest(apiUrl, apiPath, paramMap);
    
        var stock = JsonSerializer.Deserialize<StockPriceDTO>(jsonString, _jsonConfig._caseInsensitiveSerializerSettings) 
                    ?? throw new InvalidOperationException("Value cannot be null");
    
        stockPriceDto.Price = stock.Price;
        stockPriceDto.Notify();
    }
}