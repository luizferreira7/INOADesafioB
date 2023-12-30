using System.Net;
using System.Text;
using StockQuoteAlert.Business;
using StockQuoteAlert.Exceptions;

namespace Tests;

public class RequestHandlerTests
{
    private const string JSON_STOCK_STRING = "{'stock': 'PETR4', 'price': '22.3', 'status': 'Current'}";
    private const string API_URL = "url";
    private const string API_PATH = "/test";
    private Dictionary<string, string> PARAM_MAP = new Dictionary<string, string>();
    
    public static string STOCK_PARAM => "stock";
    public static string STOCK_PARAM_VALUE => "PETR4";
    public static string QUERY_PARAM => "?stock=PETR4";
    
    private const int STATUS_404 = 404;
    private const int STATUS_500 = 500;
    
    private class MockRequestHandler : ResquestHandler
    {
        private HttpResponseMessage HttpResponseMessage;


        public MockRequestHandler(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public override async Task<HttpResponseMessage> ExecuteRequest(string url)
        {
            return HttpResponseMessage;
        }
    }
    
    [Test]
    public void MakeRequestTest_Response200_MustPass()
    {
        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        StringContent stringContent = new StringContent(JSON_STOCK_STRING, Encoding.UTF8,  "text/plain");
        httpResponseMessage.Content = stringContent;

        MockRequestHandler mockRequestHandler = new MockRequestHandler(httpResponseMessage);

        var jsonString = mockRequestHandler.MakeRequest(API_URL, API_PATH, PARAM_MAP).Result;
        
        Assert.That(jsonString, Is.Not.Null);
        Assert.That(jsonString, Is.EqualTo(JSON_STOCK_STRING));
    }
    
    [Test]
    public void MakeRequestTest_Response404_MustThrowRequestHandlerExecption_Status404()
    {
        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

        MockRequestHandler mockRequestHandler = new MockRequestHandler(httpResponseMessage);

        var aggregateException = mockRequestHandler.MakeRequest(API_URL, API_PATH, PARAM_MAP).Exception;

        Assert.That(aggregateException.InnerExceptions, Is.Not.Empty);
        
        RequestErrorException requestErrorException = (RequestErrorException) aggregateException.InnerExceptions[0];
        
        Assert.That(requestErrorException, Is.Not.Null);
        Assert.That(requestErrorException.Status, Is.EqualTo(STATUS_404));
    }
    
    [Test]
    public void MakeRequestTest_Response500_MustThrowRequestHandlerExecption_Status500()
    {
        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);

        MockRequestHandler mockRequestHandler = new MockRequestHandler(httpResponseMessage);

        var aggregateException = mockRequestHandler.MakeRequest(API_URL, API_PATH, PARAM_MAP).Exception;

        Assert.That(aggregateException.InnerExceptions, Is.Not.Empty);
        
        RequestErrorException requestErrorException = (RequestErrorException) aggregateException.InnerExceptions[0];
        
        Assert.That(requestErrorException, Is.Not.Null);
        Assert.That(requestErrorException.Status, Is.EqualTo(STATUS_500));
    }

    [Test]
    public void BuildQueryParamTest_BuildCorrectParam_MustPass()
    {
        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        MockRequestHandler mockRequestHandler = new MockRequestHandler(httpResponseMessage);
        
        PARAM_MAP = new Dictionary<string, string>();
        
        PARAM_MAP.Add(STOCK_PARAM, STOCK_PARAM_VALUE);

        var queryParam = mockRequestHandler.BuildQueryParam(PARAM_MAP);
        
        Assert.That(queryParam, Is.EqualTo(QUERY_PARAM));
    }
}