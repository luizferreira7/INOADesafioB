namespace StockQuoteAlert.Constants;

public class ValidationErrorCode
{
    public static readonly ValidationErrorCode BUYPRICE_BIGGERTHAN_SELLPRICE = new ("BP01", "The buy price is bigger than the sell price.");
    public static readonly ValidationErrorCode ENVIRONMENT_VARIABLE_NULL = new ("ENV01", "The environment variable requested is null.");
    
    public string Code { get; set; }
    public string Message { get; set; }

    private ValidationErrorCode(string code, string message)
    {
        Code = code;
        Message = message;
    }

}