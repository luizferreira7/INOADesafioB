namespace MockAPI.Model;

public class StockPriceDTO
{
    public string Stock { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
 
    public StockPriceDTO()
    {
    }

    public StockPriceDTO(string stock)
    {
        Stock = stock;
    }
}