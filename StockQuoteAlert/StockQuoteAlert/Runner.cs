using System.Globalization;
using StockQuoteAlert.Constants;
using StockQuoteAlert.Exception;
using StockQuoteAlert.Model.Validators;

namespace StockQuoteAlert;

public class Runner
{
    public void Start(string[] args)
    {
        var arguments = ParseArgs(args);

        var argumentsValidator = new ArgumentsValidator(arguments);
        
        argumentsValidator.Validate();
    }

    public Arguments ParseArgs(string[] args)
    {
        Arguments arguments = new Arguments();

        try
        {
            arguments.stock = args[0];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentException(Label.STOCK, e);
        }

        try
        {
            if (double.TryParse(args[1].Replace(',', '.'), CultureInfo.InvariantCulture, out double value))
            {
                arguments.sellPrice = value;
            }
            else
            {
                throw new ParseException("double", args[1]);
            }
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentException(Label.SELL_PRICE, e);
        }

        try
        {
            if (double.TryParse(args[2].Replace(',', '.'), CultureInfo.InvariantCulture, out double value))
            {
                arguments.buyPrice = value;
            }
            else
            {
                throw new ParseException("double", args[2]);
            }
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentException(Label.BUY_PRICE, e);
        }
        
        Console.WriteLine($"{Label.STOCK}: {arguments.stock}");
        Console.WriteLine($"{Label.SELL_PRICE}: {arguments.sellPrice}");
        Console.WriteLine($"{Label.BUY_PRICE}: {arguments.buyPrice}");

        return arguments;
    }

    public Runner()
    {
    }
}