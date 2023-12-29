namespace StockQuoteAlert.Model;

public class StockPriceDTO
{
    public string? Stock { get; set; }
    public double? Price { get; set; }
    public string? Status { get; set; }
 
    public StockPriceDTO()
    {
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
}