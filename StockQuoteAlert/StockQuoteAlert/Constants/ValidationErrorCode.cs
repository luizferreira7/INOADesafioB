namespace StockQuoteAlert.Constants;

public class ValidationErrorCode
{
    public static readonly ValidationErrorCode BUYPRICE_BIGGERTHAN_SELLPRICE = new ("BP01", "The buy price is bigger than the sell price.");
    public static readonly ValidationErrorCode ENVIRONMENT_VARIABLE_NULL = new ("ENV01", "The environment variable requested is null.");
    public static readonly ValidationErrorCode ENV_FILE_NOT_FOUND = new ("ENV02", "The .env file does not exist.");
    public static readonly ValidationErrorCode STOCK_NOT_FOUND = new ("ST01", "Couldn't find the stock on API.");
    
    public static readonly ValidationErrorCode SMTP_CANT_CONNECT = new ("SMTP1", "Couldn't establish connection to the SMTP.");
    public static readonly ValidationErrorCode API_CANT_CONNECT = new ("API1", "Couldn't establish connection to the API.");
    
    public string Code { get; set; }
    public string Message { get; set; }

    private ValidationErrorCode(string code, string message)
    {
        Code = code;
        Message = message;
    }

}