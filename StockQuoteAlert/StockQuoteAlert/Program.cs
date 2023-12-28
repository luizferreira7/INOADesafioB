namespace StockQuoteAlert;

class Program
{
    static void Main(string[] args)
    {
        string stock, sellPrice, buyPrice;

        try
        {
            stock = args[0];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentExeption("Ativo");
        }

        try
        {
            sellPrice = args[1];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentExeption("Preço de venda");
        }

        try
        {
            buyPrice = args[2];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new MissingArgumentExeption("Preço de compra");
        }
        
        Console.WriteLine($"Ativo escolhido: {stock}");
        Console.WriteLine($"Preço para venda: {sellPrice}");
        Console.WriteLine($"Preço para compra: {buyPrice}");

    }
}