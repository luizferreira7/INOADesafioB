using System.Net;
using System.Text;
using StockQuoteAlert;
using StockQuoteAlert.Business;
using StockQuoteAlert.Model.Validators;
using StockQuoteAlert.Exceptions;

namespace Tests;

public class ValidatorTests
{
    private static string STOCK = "PETR4";
    private static string CODE_BP01 = "BP01";
    private const string JSON_STOCK_STRING = "{'stock': 'PETR4', 'price': '22.3', 'status': 'Current'}";

    
    private class MockRequestHandler : RequestHandler
    {
        private HttpResponseMessage HttpResponseMessage;


        public MockRequestHandler(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public override async Task<HttpResponseMessage> ExecuteRequest(string url)
        {
            return HttpResponseMessage;
        }
    }
    
    private class MockArgumentsValidator : ArgumentsValidator
    {
        public MockArgumentsValidator(Arguments arguments, MockRequestHandler mockRequestHandler) : base(arguments, "", "")
        {
            _arguments = arguments;
            _requestHandler = mockRequestHandler;
        }
    }
    
    [Test]
    public void ValidatorArgumentsTest_BuyPriceBiggerThanSellPrice_MustThrowValidationException_CodeBP01()
    {
        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        StringContent stringContent = new StringContent(JSON_STOCK_STRING, Encoding.UTF8,  "text/plain");
        httpResponseMessage.Content = stringContent;

        MockRequestHandler mockRequestHandler = new MockRequestHandler(httpResponseMessage);

        var arguments = new Arguments(STOCK, 22.56, 22.67);

        var argumentsValidator = new MockArgumentsValidator(arguments, mockRequestHandler);
        
        ValidationException? validationException = Assert.Throws<ValidationException>(() => argumentsValidator.Validate());

        Assert.That(validationException, Is.Not.Null);
        
        Assert.That(validationException.ErrorCode, Is.EqualTo(CODE_BP01));
    }
    
    [Test]
    public void ValidatorArgumentsTest_BuyPriceLesserThanSellPrice_MustPass()
    {
        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        StringContent stringContent = new StringContent(JSON_STOCK_STRING, Encoding.UTF8,  "text/plain");
        httpResponseMessage.Content = stringContent;

        MockRequestHandler mockRequestHandler = new MockRequestHandler(httpResponseMessage);

        var arguments = new Arguments(STOCK, 22.68, 22.67);

        var argumentsValidator = new MockArgumentsValidator(arguments, mockRequestHandler);
        
        Assert.DoesNotThrow(() => argumentsValidator.Validate());
    }
}