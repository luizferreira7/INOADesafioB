using System.Globalization;

namespace StockQuoteAlert;

public class Runner
{
    public void Start(string[] args)
    {
        Arguments arguments = ReadArgs(args);
    }

    public Arguments ReadArgs(string[] args)
    {
        Arguments arguments = new Arguments();

        try
        {
            arguments.stock = args[0];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentException(Constants.STOCK, e);
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
            throw new MissingArgumentException(Constants.SELL_PRICE, e);
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
            throw new MissingArgumentException(Constants.BUY_PRICE, e);
        }
        
        Console.WriteLine($"Ativo escolhido: {arguments.stock}");
        Console.WriteLine($"Preço para venda: {arguments.sellPrice}");
        Console.WriteLine($"Preço para compra: {arguments.buyPrice}");

        return arguments;
    }

    public Runner()
    {
    }
}