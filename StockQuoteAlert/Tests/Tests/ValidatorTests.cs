using StockQuoteAlert;
using StockQuoteAlert.Model.Validators;
using StockQuoteAlert.Exceptions;

namespace Tests;

public class ValidatorTests
{
    private static string STOCK = "PETR4";
    private static string CODE_BP01 = "BP01";
    
    [Test]
    public void ValidatorArgumentsTest_BuyPriceBiggerThanSellPrice_MustThrowValidationException_CodeBP01()
    {
        var arguments = new Arguments(STOCK, 22.56, 22.67);

        var argumentsValidator = new ArgumentsValidator(arguments);
        
        ValidationException? validationException = Assert.Throws<ValidationException>(() => argumentsValidator.Validate());

        Assert.That(validationException, Is.Not.Null);
        
        Assert.That(validationException.ErrorCode, Is.EqualTo(CODE_BP01));
    }
    
    [Test]
    public void ValidatorArgumentsTest_BuyPriceLesserThanSellPrice_MustPass()
    {
        var arguments = new Arguments(STOCK, 22.69, 22.67);

        var argumentsValidator = new ArgumentsValidator(arguments);
        
        Assert.DoesNotThrow(() => argumentsValidator.Validate());
    }
}