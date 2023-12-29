namespace MockAPI.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string stock) : base($"O recurso: {stock} não foi encontrado.")
    {
    }
}