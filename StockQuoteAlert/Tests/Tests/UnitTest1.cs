using StockQuoteAlert;

namespace Tests;

public class ConsoleTests
{
    private Runner runner;

    private const string PRET4 = "PETR4";
    private const double SELLPRICE = 22.67;
    private const double BUYPRICE = 22.59;
    
    [SetUp]
    public void Setup()
    {
        runner = new Runner();
    }

    [Test]
    public void RunnerArgumentsTest_NoArgument_MustThrowMissigArgumentException_ArgumentAtivo()
    {
        string[] args = [];
        
        MissingArgumentException? missingArgumentException = Assert.Throws<MissingArgumentException>(() => runner.ReadArgs(args));

        Assert.That(missingArgumentException, Is.Not.Null);
        
        Assert.That(missingArgumentException.Argument, Is.EqualTo(Label.STOCK));
    }
    
    [Test]
    public void RunnerArgumentsTest_OneArgument_MustThrowMissigArgumentException_ArgumentPrecoVenda()
    {
        string[] args = ["PETR4"];
        
        MissingArgumentException? missingArgumentException = Assert.Throws<MissingArgumentException>(() => runner.ReadArgs(args));

        Assert.That(missingArgumentException, Is.Not.Null);
        
        Assert.That(missingArgumentException.Argument, Is.EqualTo(Label.SELL_PRICE));
    }
    
    [Test]
    public void RunnerArgumentsTest_TwoArgument_MustThrowMissigArgumentException_ArgumentPrecoCompra()
    {
        string[] args = ["PETR4", "22.67"];
        
        MissingArgumentException? missingArgumentException = Assert.Throws<MissingArgumentException>(() => runner.ReadArgs(args));

        Assert.That(missingArgumentException, Is.Not.Null);
        
        Assert.That(missingArgumentException.Argument, Is.EqualTo(Label.BUY_PRICE));
    }
    
    [Test]
    public void RunnerArgumentsTest_ThreeArgumentWithDots_MustReturnArgumentsClass()
    {
        string[] args = ["PETR4", "22.67", "22.59"];
        
        Arguments arguments = runner.ReadArgs(args);
        
        Assert.That(arguments.stock, Is.EqualTo(PRET4));
        Assert.That(arguments.sellPrice, Is.EqualTo(SELLPRICE));
        Assert.That(arguments.buyPrice, Is.EqualTo(BUYPRICE));
    }
    
    [Test]
    public void RunnerArgumentsTest_ThreeArgumentWithComma_MustReturnArgumentsClass()
    {
        string[] args = ["PETR4", "22,67", "22,59"];
        
        Arguments arguments = runner.ReadArgs(args);
        
        Assert.That(arguments.stock, Is.EqualTo(PRET4));
        Assert.That(arguments.sellPrice, Is.EqualTo(SELLPRICE));
        Assert.That(arguments.buyPrice, Is.EqualTo(BUYPRICE));
    }
    
    [Test]
    public void RunnerArgumentsTest_WrongTypeForSellPrice_MustThrowParseException()
    {
        string[] args = ["PETR4", "TESTE", "22.59"];
        
        Assert.Throws<ParseException>(() => runner.ReadArgs(args));
    }
    
    [Test]
    public void RunnerArgumentsTest_WrongTypeForBuyPrice_MustThrowParseException()
    {
        string[] args = ["PETR4", "22.67", "TESTE"];
        
        Assert.Throws<ParseException>(() => runner.ReadArgs(args));
    }
}