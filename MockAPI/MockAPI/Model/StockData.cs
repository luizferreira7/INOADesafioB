namespace MockAPI.Model;

public class StockData
{
    public int Id { get; set; }
    public int Day { get; set; }
    public string Stock { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }

    public StockData()
    {
    }

    public StockData(int id, int day, string stock, double open, double high, double low, double close)
    {
        Id = id;
        Day = day;
        Stock = stock;
        Open = open;
        High = high;
        Low = low;
        Close = close;
    }
    
    public override string ToString()
    {
        return $"Day: {Day}, Stock: {Stock}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}";
    }
}