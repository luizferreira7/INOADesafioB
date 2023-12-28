namespace StockQuoteAlert;

public class MissingArgumentExeption : Exception
{
    public string Argument { get; }

    public MissingArgumentExeption(string argument)
        : base($"Missing argument on program execution. Argument: {argument}")
    {
        Argument = argument;
    }

    public MissingArgumentExeption(string argument, Exception inner)
        : base($"Missing argument on program execution. Argument: {argument}", inner)
    {
        Argument = argument;
    }
}