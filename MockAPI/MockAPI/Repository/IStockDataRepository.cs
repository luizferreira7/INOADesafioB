using MockAPI.Model;

namespace MockAPI.Repository;

public interface IStockDataRepository
{
    public List<StockData> GetStocks();
    public StockData? GetStockByDayAndStock(int day, string stock);
    public void InitStockDataInMemoryDb();
}