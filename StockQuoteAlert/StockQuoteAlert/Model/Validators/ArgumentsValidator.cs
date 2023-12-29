using StockQuoteAlert.Constants;
using StockQuoteAlert.Exception;

namespace StockQuoteAlert.Model.Validators;

public class ArgumentsValidator : IValidator
{
    private Arguments Arguments { get; set; }
    
    public ArgumentsValidator(Arguments arguments)
    {
        Arguments = arguments;
    }

    public void Validate()
    {
        BuyPriceBiggerThanSellPrice();
    }

    private void BuyPriceBiggerThanSellPrice()
    {
        if (Arguments.sellPrice > Arguments.buyPrice)
        {
            throw new ValidationException(ValidationErrorCode.BUYPRICE_BIGGERTHAN_SELLPRICE);
        }
    }
}