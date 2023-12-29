namespace StockQuoteAlert.Exception;
using System;

public class RequestErrorException : Exception
{
    public string? Status { get; }

    public RequestErrorException()
        : base("Request failed.")
    {
    }
    
    public RequestErrorException(Exception inner)
        : base("Request failed.", inner)
    {
    }
    
    public RequestErrorException(string status)
        : base($"Request failed with status: {status}")
    {
        Status = status;
    }
}