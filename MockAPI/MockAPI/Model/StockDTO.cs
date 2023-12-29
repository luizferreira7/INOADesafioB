namespace MockAPI.Model;

public class StockDTO
{
    public int Date { get; set; }
    public string Stock { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }

    public StockDTO()
    {
    }

    public StockDTO(int date, string stock, double open, double high, double low, double close)
    {
        Date = date;
        Stock = stock;
        Open = open;
        High = high;
        Low = low;
        Close = close;
    }
}