using MockAPI.Model;

namespace MockAPI.Repository;

public interface IStockDataRepository
{
    public List<StockData> GetStocks();
    public void InitStockDataInMemoryDb();
}