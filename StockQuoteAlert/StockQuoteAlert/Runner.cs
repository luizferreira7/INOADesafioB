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
            throw new MissingArgumentExeption(Constants.STOCK, e);
        }

        try
        {
            arguments.sellPrice = args[1];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentExeption(Constants.SELL_PRICE, e);
        }

        try
        {
            arguments.buyPrice = args[2];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentExeption(Constants.BUY_PRICE, e);
        }
        
        Console.WriteLine($"Ativo escolhido: {arguments.stock}");
        Console.WriteLine($"Preço para venda: {arguments.sellPrice}");
        Console.WriteLine($"Preço para compra: {arguments.buyPrice}");

        return arguments;
    }
}