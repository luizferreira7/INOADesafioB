namespace StockQuoteAlert;

class Program
{
    public static void Main(string[] args)
    {
        var currentDomain = AppDomain.CurrentDomain;
        currentDomain.UnhandledException += (Handler);
        
        var runner = new Runner();
        runner.Start(args);
    }
    
    private static void Handler(object sender, UnhandledExceptionEventArgs args)
    {
        Exception e = (Exception) args.ExceptionObject;
        
        Console.WriteLine(e.Message);
        Console.WriteLine(e.StackTrace);
    }
}