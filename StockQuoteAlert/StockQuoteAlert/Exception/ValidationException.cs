using StockQuoteAlert.Constants;

namespace StockQuoteAlert.Exception;
using System;

public class ValidationException : Exception
{
    public string ErrorCode { get; }

    public ValidationException(ValidationErrorCode validationErrorCode)
        : base(validationErrorCode.Message)
    {
        ErrorCode = validationErrorCode.Code;
    }

    public ValidationException(ValidationErrorCode validationErrorCode, Exception inner)
        : base(validationErrorCode.Message, inner)
    {
        ErrorCode = validationErrorCode.Code;
    }
}