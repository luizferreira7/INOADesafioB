using System.Net;
using StockQuoteAlert.Constants;
using StockQuoteAlert.Exceptions;
using StockQuoteAlert.Business;
using StockQuoteAlert.Utility;

namespace StockQuoteAlert.Model.Validators;

public class ArgumentsValidator : IValidator
{
    protected Arguments _arguments;
    protected RequestHandler _requestHandler;

    protected string _apiUrl;
    
    protected string _apiPath;
    
    public ArgumentsValidator(Arguments arguments, string apiUrl, string apiPath)
    {
        _arguments = arguments;
        _requestHandler = new RequestHandler();
        _apiUrl = apiUrl;
        _apiPath = apiPath;
    }

    public void Validate()
    {
        BuyPriceBiggerThanSellPrice();
        StockExists();
    }

    private void BuyPriceBiggerThanSellPrice()
    {
        if (_arguments.BuyPrice > _arguments.SellPrice)
        {
            throw new ValidationException(ValidationErrorCode.BUYPRICE_BIGGERTHAN_SELLPRICE);
        }
    }

    private async void StockExists()
    {
        var paramMap = new Dictionary<string, string>();

        paramMap.Add(Params.STOCK_PARAM, _arguments.Stock);
        
        try
        {
            await _requestHandler.MakeRequest(_apiUrl, _apiPath, paramMap);
        }
        catch (RequestErrorException e)
        {
            if (e.Status == (int) HttpStatusCode.NotFound)
            {
                throw new ValidationException(ValidationErrorCode.STOCK_NOT_FOUND);
            }
        }
    }
}