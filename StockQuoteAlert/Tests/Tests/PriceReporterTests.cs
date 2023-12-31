using StockQuoteAlert;
using StockQuoteAlert.Business;
using StockQuoteAlert.Model;

namespace Tests;

public class PriceReporterTests
{
    private const string PRET4 = "PETR4";
    private const double SELLPRICE = 22.67;
    private const double BUYPRICE = 22.59;
    
    private const double PRICE_BETWEEN_TARGET = 22.62;
    
    private const double PRICE_TO_SELL = 22.89;
    private const string EMAIL_SUBJECT_TO_SELL = "PETR4 Sell Alert";
    private const string EMAIL_BODY_TO_SELL = $"The selected stock PETR4 has reached the target sell price.\n\n " +
                                              $"Target price: 22,67,\n Current price: 22,89";
    
    private const double PRICE_TO_BUY = 22.19;
    private const string EMAIL_SUBJECT_TO_BUY = "PETR4 Buy Alert";
    private const string EMAIL_BODY_TO_BUY = $"The selected stock PETR4 has reached the target buy price.\n\n " +
                                              $"Target price: 22,59,\n Current price: 22,19";
    
    private class MockSender : Sender
    {
        public MockSender()
        {
        }

        protected override void ReadEnv()
        {
        }

        public override void SendEmail(string subject, string body)
        {
        }
    }
    
    private class MockPriceReporter : PriceReporter
    {
        private static MockSender _sender = new MockSender();

        public MockPriceReporter(Arguments arguments) : base(arguments, _sender)
        {
        }
    }
    
    [Test]
    public void PriceReporterTest_NoTargetPriceReached_MustAssertFalse()
    {
        var arguments = new Arguments(PRET4, SELLPRICE, BUYPRICE);
        var stock = new StockPriceDTO(PRET4, SELLPRICE);

        var mockPriceReporter = new MockPriceReporter(arguments);
        
        stock.Attach(mockPriceReporter);

        stock.Price = PRICE_BETWEEN_TARGET;
        
        stock.Notify();

        Assert.That(mockPriceReporter._targetReached, Is.False);
    }
    
    [Test]
    public void PriceReporterTest_SellTargetPriceReached_MustAssertTrue()
    {
        var arguments = new Arguments(PRET4, SELLPRICE, BUYPRICE);
        var stock = new StockPriceDTO(PRET4, SELLPRICE);

        var mockPriceReporter = new MockPriceReporter(arguments);
        
        stock.Attach(mockPriceReporter);

        stock.Price = PRICE_TO_SELL;
        
        stock.Notify();

        Assert.That(mockPriceReporter._targetReached, Is.True);
        Assert.That(mockPriceReporter._emailSubject, Is.EqualTo(EMAIL_SUBJECT_TO_SELL));
        Assert.That(mockPriceReporter._emailBody, Is.EqualTo(EMAIL_BODY_TO_SELL));
    }
    
    [Test]
    public void PriceReporterTest_BuyTargetPriceReached_MustAssertTrue()
    {
        var arguments = new Arguments(PRET4, SELLPRICE, BUYPRICE);
        var stock = new StockPriceDTO(PRET4, SELLPRICE);

        var mockPriceReporter = new MockPriceReporter(arguments);
        
        stock.Attach(mockPriceReporter);

        stock.Price = PRICE_TO_BUY;
        
        stock.Notify();

        Assert.That(mockPriceReporter._targetReached, Is.True);
        Assert.That(mockPriceReporter._emailSubject, Is.EqualTo(EMAIL_SUBJECT_TO_BUY));
        Assert.That(mockPriceReporter._emailBody, Is.EqualTo(EMAIL_BODY_TO_BUY));
    }
}