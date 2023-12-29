using Microsoft.EntityFrameworkCore;
using MockAPI;
using MockAPI.Exceptions;
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

app.UseExceptionHandler(exceptionHandlerApp 
    => exceptionHandlerApp.Run(async context 
        => await Results.Problem()
            .ExecuteAsync(context)));

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
        var service = new StockDataService(stockDataRepository);

        try
        {
            var stockPrice = service.GetStockPriceDTO(stock);

            return Results.Ok(stockPrice);
        }
        catch (ResourceNotFoundException e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
            
            return Results.NotFound();
        }
    })
    .WithName("GetStockPrice")
    .WithOpenApi(stockPriceDto => stockPriceDto);

app.Run();
