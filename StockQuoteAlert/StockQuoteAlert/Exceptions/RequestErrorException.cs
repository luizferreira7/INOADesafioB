namespace StockQuoteAlert.Exceptions;

public class RequestErrorException : Exception
{
    public int? Status { get; }

    public RequestErrorException()
        : base("Request failed.")
    {
    }
    
    public RequestErrorException(Exception inner)
        : base("Request failed.", inner)
    {
    }
    
    public RequestErrorException(int status)
        : base($"Request failed with status: {status}")
    {
        Status = status;
    }
}