using StockQuoteAlert.Exceptions;
using StockQuoteAlert.Utility;

namespace Tests;

public class DotEnvLoaderTests
{
    private const string API_URL_KEY = "API_URL";
    private const string API_URL_VALUE = "url";
    private const string API_PATH_KEY = "API_PATH";
    private const string CODE_ENV01 = "ENV01";
    
    private class MockDotEnvLoader : DotEnvLoader
    {
        public MockDotEnvLoader()
        {
            Load();
        }

        public override void Load()
        {
            Environment.SetEnvironmentVariable(API_URL_KEY, API_URL_VALUE);
        }
    }
    
    [Test]
    public void DotEnvLoaderTest_GetEnvByKey_KeyExist_MustReturnString()
    {
        var mockEnvLoader = new MockDotEnvLoader();

        var env = mockEnvLoader.GetEnvByKey(API_URL_KEY);
        
        Assert.That(env, Is.EqualTo(API_URL_VALUE));
    }
    
    [Test]
    public void DotEnvLoaderTest_GetEnvByKey_KeyNotExist_MustThrowValidationException()
    {
        var mockEnvLoader = new MockDotEnvLoader();
        
        ValidationException? validationException = Assert.Throws<ValidationException>(() => mockEnvLoader.GetEnvByKey(API_PATH_KEY));

        Assert.That(validationException, Is.Not.Null);
        
        Assert.That(validationException.ErrorCode, Is.EqualTo(CODE_ENV01));
    }
    
}