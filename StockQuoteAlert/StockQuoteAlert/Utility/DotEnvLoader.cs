using StockQuoteAlert.Constants;
using StockQuoteAlert.Exception;

namespace StockQuoteAlert.Utility;

public class DotEnvLoader
{
    public DotEnvLoader()
    {
        DotNetEnv.Env.Load();
    }

    public string GetEnvByKey(string key)
    {
        var env = Environment.GetEnvironmentVariable(key);

        if (env is null)
        {
            throw new ValidationException(ValidationErrorCode.ENVIRONMENT_VARIABLE_NULL, key);
        }

        return env;
    }
}