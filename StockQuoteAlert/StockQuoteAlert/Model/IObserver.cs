namespace StockQuoteAlert.Model;


public interface IObserver
{
    void Update(ISubject subject);
}