using StockQuoteAlert.Model;

namespace StockQuoteAlert.Business;

public class PriceReporter : IObserver
{
    private readonly Arguments _arguments;
    private Sender _sender;
    
    public PriceReporter(Arguments arguments)
    {
        _arguments = arguments;
        _sender = new Sender();
    }
    
    public void Update(ISubject subject)
    {
        checkSellPrice(subject);
        checkBuyPrice(subject);
    }

    public void checkBuyPrice(ISubject subject)
    {
        var stockPriceDto = (StockPriceDTO) subject;
        
        if (stockPriceDto.Price <= _arguments.BuyPrice)
        {
            Console.WriteLine("Time to buy.");
            _sender.SendEmail($"{_arguments.Stock} Buy Alert", 
                $"The selected stock has reached the target buy price.\n\n " +
                $"Current price: {stockPriceDto.Price}, Buy price: {_arguments.BuyPrice}");
        }
    }

    public void checkSellPrice(ISubject subject)
    {
        var stockPriceDto = (StockPriceDTO) subject;
        
        if (stockPriceDto.Price >= _arguments.SellPrice)
        {
            Console.WriteLine("Time to sell.");
            _sender.SendEmail($"{_arguments.Stock} Sell Alert", 
                $"The selected stock has reached the target sell price.\n\n " +
                $"Current price: {stockPriceDto.Price}, Sell price: {_arguments.SellPrice}");
        }
    }
}