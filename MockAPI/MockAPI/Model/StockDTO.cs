namespace MockAPI.Model;

public class StockDTO
{
    public string Stock { get; set; }
    public double Price { get; set; }

    public StockDTO()
    {
    }

    public StockDTO(string stock, double price)
    {
        Stock = stock;
        Price = price;
    }
}