using StockQuoteAlert;
using StockQuoteAlert.Exceptions;
using StockQuoteAlert.Business;

namespace Tests;

public class ParserTests
{
    private Parser parser;
    
    private const string STOCK = "Stock";
    private const string SELL_PRICE = "Sell price";
    private const string BUY_PRICE = "Buy price";

    private const string PRET4 = "PETR4";
    private const double SELLPRICE = 22.67;
    private const double BUYPRICE = 22.59;
    
    [SetUp]
    public void Setup()
    {
        parser = new Parser();
    }

    [Test]
    public void ParserArgsTest_NoArgument_MustThrowMissigArgumentException_ArgumentAtivo()
    {
        string[] args = [];
        
        MissingArgumentException? missingArgumentException = Assert.Throws<MissingArgumentException>(() => parser.ParseArgs(args));

        Assert.That(missingArgumentException, Is.Not.Null);
        
        Assert.That(missingArgumentException.Argument, Is.EqualTo(STOCK));
    }
    
    [Test]
    public void ParserArgsTest_OneArgument_MustThrowMissigArgumentException_ArgumentPrecoVenda()
    {
        string[] args = ["PETR4"];
        
        MissingArgumentException? missingArgumentException = Assert.Throws<MissingArgumentException>(() => parser.ParseArgs(args));

        Assert.That(missingArgumentException, Is.Not.Null);
        
        Assert.That(missingArgumentException.Argument, Is.EqualTo(SELL_PRICE));
    }
    
    [Test]
    public void ParserArgsTest_TwoArgument_MustThrowMissigArgumentException_ArgumentPrecoCompra()
    {
        string[] args = ["PETR4", "22.67"];
        
        MissingArgumentException? missingArgumentException = Assert.Throws<MissingArgumentException>(() => parser.ParseArgs(args));

        Assert.That(missingArgumentException, Is.Not.Null);
        
        Assert.That(missingArgumentException.Argument, Is.EqualTo(BUY_PRICE));
    }
    
    [Test]
    public void ParserArgsTest_ThreeArgumentWithDots_MustReturnArgumentsClass()
    {
        string[] args = ["PETR4", "22.67", "22.59"];
        
        Arguments arguments = parser.ParseArgs(args);
        
        Assert.That(arguments.Stock, Is.EqualTo(PRET4));
        Assert.That(arguments.SellPrice, Is.EqualTo(SELLPRICE));
        Assert.That(arguments.BuyPrice, Is.EqualTo(BUYPRICE));
    }
    
    [Test]
    public void ParserArgsTest_ThreeArgumentWithComma_MustReturnArgumentsClass()
    {
        string[] args = ["PETR4", "22,67", "22,59"];
        
        Arguments arguments = parser.ParseArgs(args);
        
        Assert.That(arguments.Stock, Is.EqualTo(PRET4));
        Assert.That(arguments.SellPrice, Is.EqualTo(SELLPRICE));
        Assert.That(arguments.BuyPrice, Is.EqualTo(BUYPRICE));
    }
    
    [Test]
    public void ParserArgsTest_WrongTypeForSellPrice_MustThrowParseException()
    {
        string[] args = ["PETR4", "TESTE", "22.59"];
        
        Assert.Throws<ParseException>(() => parser.ParseArgs(args));
    }
    
    [Test]
    public void ParserArgsTest_WrongTypeForBuyPrice_MustThrowParseException()
    {
        string[] args = ["PETR4", "22.67", "TESTE"];
        
        Assert.Throws<ParseException>(() => parser.ParseArgs(args));
    }
}