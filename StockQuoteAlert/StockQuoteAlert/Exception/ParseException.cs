namespace StockQuoteAlert.Exception;
using System;

public class ParseException : Exception
{
    public string OriginType { get; }
    
    public string TargetType { get; }
    
    public object Value { get; }

    public ParseException(string targetType, object value)
        : base($"Could not parse the given value: {value} from type: {value.GetType()} to: {targetType}")
    {
        OriginType = value.GetType().ToString();
        TargetType = targetType;
        Value = value;
    }

    public ParseException(string targetType, object value, Exception inner)
        : base($"Could not parse the given value: {value} from type: {value.GetType()} to: {targetType}", inner)
    {
        OriginType = value.GetType().ToString();
        TargetType = targetType;
        Value = value;
    }
}