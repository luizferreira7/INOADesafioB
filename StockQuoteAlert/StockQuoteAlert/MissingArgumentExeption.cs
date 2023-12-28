namespace StockQuoteAlert;

public class MissingArgumentExeption : Exception
{
    public MissingArgumentExeption()
    {
    }

    public MissingArgumentExeption(string argument)
        : base($"Missing argument on program execution. Argument: {argument}")
    {
    }

    public MissingArgumentExeption(string argument, Exception inner)
        : base($"Missing argument on program execution. Argument: {argument}", inner)
    {
    }
}