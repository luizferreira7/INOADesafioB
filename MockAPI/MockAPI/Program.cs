using Microsoft.EntityFrameworkCore;
using MockAPI;
using MockAPI.Model;
using MockAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StockDataDb>(opt => opt.UseInMemoryDatabase("StockData"));
builder.Services.AddScoped<StockDataDb>();
builder.Services.AddScoped<IStockDataRepository, StockDataRepository>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

var stockDataDb = app.Services.CreateScope().ServiceProvider.GetRequiredService<StockDataDb>();
await stockDataDb.Database.EnsureCreatedAsync();

var stockDataRepository = new StockDataRepository(stockDataDb);
stockDataRepository.InitStockDataInMemoryDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/finance/stock_price", (string stock) =>
    {
        List<StockData> stocks = stockDataRepository.GetStocks();
        
        Console.WriteLine(stocks[0]);
        
        return stock;
    })
    .WithName("GetStockPrice")
    .WithOpenApi();

app.Run();
