namespace StockQuoteAlert;

public class Arguments
{
    public string? stock { get; set; }
    public double? sellPrice { get; set; }
    public double? buyPrice { get; set; }

    public Arguments()
    {
    }

    public Arguments(string? stock, double? sellPrice, double? buyPrice)
    {
        this.stock = stock;
        this.sellPrice = sellPrice;
        this.buyPrice = buyPrice;
    }
}