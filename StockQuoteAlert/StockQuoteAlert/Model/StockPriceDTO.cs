namespace StockQuoteAlert.Model;

public class StockPriceDTO : ISubject
{
    public string? Stock { get; set; }
    public double? Price { get; set; }
    
    private List<IObserver> _observers = new List<IObserver>();
 
    public StockPriceDTO()
    {
    }
    
    public StockPriceDTO(string stock)
    {
        Stock = stock;
    }

    public StockPriceDTO(string stock, double price)
    {
        Stock = stock;
        Price = price;
    }
    
    public override string ToString()
    {
        return $"Stock: {Stock}, Price: {Price}";
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
        Console.Write("Stock Price: Attached an observer.");
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
        Console.Write("Stock Price: Detached an observer.");
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}