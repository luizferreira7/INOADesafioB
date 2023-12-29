using System.Globalization;
using MockAPI.Model;
using CsvHelper;
using CsvHelper.Configuration;

namespace MockAPI.Repository;

public class StockRepository
{
    public List<StockDTO> Stocks { get; set; }

    public StockRepository()
    {
        const string filename = "COTAHIST_2023.csv";
        var csvPath = Path.Combine(Environment.CurrentDirectory, @"Resources/", filename);
        Stocks = GetAllStockData(csvPath);
    }

    public List<StockDTO> GetAllStockData(string csvPath)
    {
        using (var reader = new StreamReader(csvPath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<StockDataMap>();
            var records = csv.GetRecords<StockDTO>().ToList();
            return records;
        }
    }
    
    private sealed class StockDataMap : ClassMap<StockDTO>
    {
        public StockDataMap()
        {
            Map(s => s.Date).Name("date");
            Map(s => s.Stock).Name("stock");
            Map(s => s.Open).Name("open");
            Map(s => s.High).Name("high");
            Map(s => s.Low).Name("low");
            Map(s => s.Close).Name("close");
        }
    }
}