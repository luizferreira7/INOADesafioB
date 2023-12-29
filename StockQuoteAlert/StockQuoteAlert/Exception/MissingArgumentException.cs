namespace StockQuoteAlert.Exception;
using System;

public class MissingArgumentException : Exception
{
    public string Argument { get; }

    public MissingArgumentException(string argument)
        : base($"Missing argument on program execution. Argument: {argument}")
    {
        Argument = argument;
    }

    public MissingArgumentException(string argument, Exception inner)
        : base($"Missing argument on program execution. Argument: {argument}", inner)
    {
        Argument = argument;
    }
}