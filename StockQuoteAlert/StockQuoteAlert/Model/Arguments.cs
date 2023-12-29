namespace StockQuoteAlert;

public class Arguments
{
    public string Stock { get; set; }
    public double SellPrice { get; set; }
    public double BuyPrice { get; set; }

    public Arguments()
    {
        Stock = "";
    }

    public Arguments(string stock, double sellPrice, double buyPrice)
    {
        Stock = stock;
        SellPrice = sellPrice;
        BuyPrice = buyPrice;
    }
}