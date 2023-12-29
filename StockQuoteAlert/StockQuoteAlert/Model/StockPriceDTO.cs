namespace StockQuoteAlert.Model;

public class StockPriceDTO : ISubject
{
    public string? Stock { get; set; }
    public double? Price { get; set; }
    public string? Status { get; set; }
    
    private List<IObserver> _observers = new List<IObserver>();
 
    public StockPriceDTO()
    {
    }
    
    public StockPriceDTO(string stock)
    {
        Stock = stock;
    }

    public StockPriceDTO(string stock, double price, string status)
    {
        Stock = stock;
        Price = price;
        Status = status;
    }
    
    public override string ToString()
    {
        return $"Stock: {Stock}, Price: {Price}, Status: {Status}";
    }

    public void Attach(IObserver observer)
    {
        Console.WriteLine("Subject: Attached an observer.");
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine("Subject: Detached an observer.");
    }

    public void Notify()
    {
        Console.WriteLine("Subject: Notifying observers...");

        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}