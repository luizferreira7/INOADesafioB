using StockQuoteAlert.Model;

namespace StockQuoteAlert.Business;

public class PriceReporter : IObserver
{
    protected readonly Arguments _arguments;
    private Sender _sender;
    public string _emailSubject { get; set; } = "";
    public string _emailBody { get; set; } = "";
    public bool _targetReached { get; set; }

    public PriceReporter(Arguments arguments, Sender sender)
    {
        _arguments = arguments;
        _sender = sender;
        _targetReached = false;
    }
    
    public void Update(ISubject subject)
    {
        _targetReached = false;
        
        checkSellPrice(subject);
        checkBuyPrice(subject);

        if (_targetReached)
        {
            _sender.SendEmail(_emailSubject, _emailBody);
        }
        else
        {
            Console.Write("The target prices have not been met.");
        }
    }

    private void checkBuyPrice(ISubject subject)
    {
        var stockPriceDto = (StockPriceDTO) subject;
        
        if (stockPriceDto.Price <= _arguments.BuyPrice)
        {
            Console.WriteLine("Time to buy.");
            _emailSubject = $"{_arguments.Stock} Buy Alert";
            _emailBody = $"The selected stock {_arguments.Stock} has reached the target buy price.\n\n " +
                         $"Target price: {_arguments.BuyPrice},\n Current price: {stockPriceDto.Price}";
            _targetReached = true;
        }
    }

    private void checkSellPrice(ISubject subject)
    {
        var stockPriceDto = (StockPriceDTO) subject;
        
        if (stockPriceDto.Price >= _arguments.SellPrice)
        {
            Console.WriteLine("Time to sell.");
            _emailSubject = $"{_arguments.Stock} Sell Alert";
            _emailBody = $"The selected stock {_arguments.Stock} has reached the target sell price.\n\n " +
                         $"Target price: {_arguments.SellPrice},\n Current price: {stockPriceDto.Price}";
            _targetReached = true;
        }
    }
}