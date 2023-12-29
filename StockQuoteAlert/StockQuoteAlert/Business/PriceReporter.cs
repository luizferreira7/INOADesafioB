using StockQuoteAlert.Model;

namespace StockQuoteAlert.Business;

public class PriceReporter : IObserver
{
    private Arguments Arguments;
    
    public PriceReporter(Arguments arguments)
    {
        Arguments = arguments;
    }
    
    public void Update(ISubject subject)
    {
        checkSellPrice(subject);
        checkBuyPrice(subject);
    }

    public void checkBuyPrice(ISubject subject)
    {
        var stockPriceDto = (StockPriceDTO) subject;
        
        if (stockPriceDto.Price <= Arguments.BuyPrice)
        {
            Console.WriteLine("Time to buy.");
        }
    }

    public void checkSellPrice(ISubject subject)
    {
        var stockPriceDto = (StockPriceDTO) subject;
        
        if (stockPriceDto.Price >= Arguments.SellPrice)
        {
            Console.WriteLine("Time to sell.");
        }
    }
}