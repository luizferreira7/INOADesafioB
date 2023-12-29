using System.Globalization;
using MockAPI.Model;
using CsvHelper;
using CsvHelper.Configuration;

namespace MockAPI.Repository;

public class StockDataRepository : IStockDataRepository
{

    private readonly StockDataDb _dbContext;

    public StockDataRepository(StockDataDb stockDataDb)
    {
        _dbContext = stockDataDb;
    }

    public StockData? GetStockByDayAndStock(int day, string stock)
    {
        List<StockData> stocksData = GetStocks();
        
        foreach (var stockData in stocksData)
        {
            if (stockData.Day == day && stockData.Stock == stock)
            {
                return stockData;
            }
        }

        return null;
    }
    
    public List<StockData> GetStocks()
    {
        return _dbContext.Stocks.ToList();
    }
    
    public void InitStockDataInMemoryDb()
    { 
        const string filename = "COTAHIST_2023.csv";
        var csvPath = Path.Combine(Environment.CurrentDirectory, @"Resources/", filename);

        var stocks = GetAllStockDataFromCsv(csvPath);
        
        _dbContext.Stocks.AddRange(stocks);
        _dbContext.SaveChanges();
    }

    public List<StockData> GetAllStockDataFromCsv(string csvPath)
    {
        using (var reader = new StreamReader(csvPath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<StockDataMap>();
            var records = csv.GetRecords<StockData>().ToList();
            return records;
        }
    }
    
    private sealed class StockDataMap : ClassMap<StockData>
    {
        public StockDataMap()
        {
            Map(s => s.Day).Name("day");
            Map(s => s.Stock).Name("stock");
            Map(s => s.Open).Name("open");
            Map(s => s.High).Name("high");
            Map(s => s.Low).Name("low");
            Map(s => s.Close).Name("close");
        }
    }
}