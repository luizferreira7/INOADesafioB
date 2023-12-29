using MockAPI.Constants;
using MockAPI.Exceptions;
using MockAPI.Model;
using MockAPI.Repository;

namespace MockAPI.Service;

public sealed class StockDataService(IStockDataRepository stockDataRepository)
{

    public StockPriceDTO GetStockPriceDTO(string stock)
    {
        var date = DateTime.Now;

        var day = date.Day;
        
        var stockData = stockDataRepository.GetStockByDayAndStock(day, stock);

        if (stockData is null)
        {
            throw new ResourceNotFoundException(stock);
        }

        var stockPriceDto = new StockPriceDTO(stock);

        switch (true)
        {
            case var _ when IsPreOpen(date):
                stockPriceDto.Price = stockData.Open;
                stockPriceDto.Status = Status.OPEN;
                break;

            case var _ when IsClose(date):
                stockPriceDto.Price = stockData.Close;
                stockPriceDto.Status = Status.CLOSE;
                break;

            default:
                var random = new Random();
                stockPriceDto.Price = stockData.Low + random.NextDouble() * (stockData.High - stockData.Low);
                stockPriceDto.Status = Status.CURRENT;
                break;
        }

        return stockPriceDto;
    }

    private static bool IsPreOpen(DateTime date)
    {
        return date.Hour == 9 & date.Minute >= 45;
    }

    private static bool IsClose(DateTime date)
    {
        return date.Hour >= 17 | date.Hour < 10;
    }
}