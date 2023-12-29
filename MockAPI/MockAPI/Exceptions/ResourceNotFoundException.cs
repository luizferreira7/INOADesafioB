namespace MockAPI.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string stock) : base($"Resource: {stock} not found.")
    {
    }
}