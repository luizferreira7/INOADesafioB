using MockAPI.Exceptions;
using MockAPI.Model;
using MockAPI.Repository;

namespace MockAPI;

public sealed class StockDataService(IStockDataRepository stockDataRepository)
{
    public StockPriceDTO GetStockPriceDTO(string stock)
    {
        DateTime date = DateTime.Now;

        int day = date.Day;
        
        StockData? stockData = stockDataRepository.GetStockByDayAndStock(day, stock);

        if (stockData is null)
        {
            throw new ResourceNotFoundException(stock);
        }

        StockPriceDTO stockPriceDto = new StockPriceDTO(stock);

        if (date.Hour >= 17 || date.Hour < 10 || (date.Hour == 10 && date.Minute == 0))
        {
            stockPriceDto.Price = stockData.Close;
        }
        else if (date.Hour == 9 && date.Minute >= 45)
        {
            stockPriceDto.Price = stockData.Open;
        }
        else
        {
            var random = new Random();
            
            var price = stockData.Low + random.NextDouble() * (stockData.High - stockData.Low);

            stockPriceDto.Price = price;
        }

        return stockPriceDto;
    }
}