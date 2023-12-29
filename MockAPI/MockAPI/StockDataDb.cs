using Microsoft.EntityFrameworkCore;
using MockAPI.Model;

namespace MockAPI;

public class StockDataDb : DbContext
{
    public StockDataDb(DbContextOptions<StockDataDb> options)
        : base(options) { }

    public DbSet<StockData> Stocks { get; set; }
}