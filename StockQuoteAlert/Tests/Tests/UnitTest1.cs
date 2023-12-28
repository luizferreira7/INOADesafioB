using StockQuoteAlert;

namespace Tests;

public class ConsoleTests
{
    private Runner runner;

    private const string PRET4 = "PETR4";
    private const string SELLPRICE = "22.67";
    private const string BUYPRICE = "22.59";
    
    [SetUp]
    public void Setup()
    {
        runner = new Runner();
    }

    [Test]
    public void RunnerArgumentsTest_NoArgument_MustThrowMissigArgumentExeception_ArgumentAtivo()
    {
        string[] args = [];
        
        MissingArgumentExeption? missingArgumentExeption = Assert.Throws<MissingArgumentExeption>(() => runner.ReadArgs(args));

        Assert.That(missingArgumentExeption, Is.Not.Null);
        
        Assert.That(missingArgumentExeption.Argument, Is.EqualTo(Constants.STOCK));
    }
    
    [Test]
    public void RunnerArgumentsTest_OneArgument_MustThrowMissigArgumentExeception_ArgumentPrecoVenda()
    {
        string[] args = ["PETR4"];
        
        MissingArgumentExeption? missingArgumentExeption = Assert.Throws<MissingArgumentExeption>(() => runner.ReadArgs(args));

        Assert.That(missingArgumentExeption, Is.Not.Null);
        
        Assert.That(missingArgumentExeption.Argument, Is.EqualTo(Constants.SELL_PRICE));
    }
    
    [Test]
    public void RunnerArgumentsTest_TwoArgument_MustThrowMissigArgumentExeception_ArgumentPrecoCompra()
    {
        string[] args = ["PETR4", "22.67"];
        
        MissingArgumentExeption? missingArgumentExeption = Assert.Throws<MissingArgumentExeption>(() => runner.ReadArgs(args));

        Assert.That(missingArgumentExeption, Is.Not.Null);
        
        Assert.That(missingArgumentExeption.Argument, Is.EqualTo(Constants.BUY_PRICE));
    }
    
    [Test]
    public void RunnerArgumentsTest_ThreeArgument_MustReturnArgumentsClass()
    {
        string[] args = ["PETR4", "22.67", "22.59"];
        
        Arguments arguments = runner.ReadArgs(args);
        
        Assert.That(arguments.stock, Is.EqualTo(PRET4));
        Assert.That(arguments.sellPrice, Is.EqualTo(SELLPRICE));
        Assert.That(arguments.buyPrice, Is.EqualTo(BUYPRICE));
    }
}