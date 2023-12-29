namespace StockQuoteAlert.Constants;

public class ValidationErrorCode
{
    public static readonly ValidationErrorCode BUYPRICE_BIGGERTHAN_SELLPRICE = new ("BP01", "O preço de compra é maior que o de venda");
    
    public string Code { get; set; }
    public string Message { get; set; }

    private ValidationErrorCode(string code, string message)
    {
        Code = code;
        Message = message;
    }

}