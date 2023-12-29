using StockQuoteAlert.Constants;

namespace StockQuoteAlert.Exception;
using System;

public class ValidationException : Exception
{
    public string ErrorCode { get; }
    public string Value { get; }

    public ValidationException(ValidationErrorCode validationErrorCode)
        : base(validationErrorCode.Message)
    {
        ErrorCode = validationErrorCode.Code;
    }

    public ValidationException(ValidationErrorCode validationErrorCode, string value)
        : base(validationErrorCode.Message + $" Value: {value}")
    {
        ErrorCode = validationErrorCode.Code;
        Value = value;
    }
}