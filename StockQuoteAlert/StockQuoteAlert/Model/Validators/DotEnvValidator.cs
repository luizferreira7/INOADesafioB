using StockQuoteAlert.Constants;
using StockQuoteAlert.Exceptions;
using StockQuoteAlert.Utility;

namespace StockQuoteAlert.Model.Validators;

public class DotEnvValidator : IValidator
{
    private string filePath;
    
    public DotEnvValidator()
    {
        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
    }

    public void Validate()
    {
        DotEnvFileExists();
        AllEnvExists();
    }

    private void DotEnvFileExists()
    {
        if (!File.Exists(filePath))
        {
            throw new ValidationException(ValidationErrorCode.ENV_FILE_NOT_FOUND);
        }
    }

    private void AllEnvExists()
    {
        var envLoader = new DotEnvLoader();
        
        envLoader.GetEnvByKey(EnvironmentVariables.API_URL);
        envLoader.GetEnvByKey(EnvironmentVariables.API_STOCK_PATH);
        
        envLoader.GetEnvByKey(EnvironmentVariables.SMTP_SERVER);
        var port = envLoader.GetEnvByKey(EnvironmentVariables.SMTP_PORT);

        if (!int.TryParse(port, out _))
        {
            throw new ParseException("int", port);
        }
        
        envLoader.GetEnvByKey(EnvironmentVariables.SMTP_USER);
        envLoader.GetEnvByKey(EnvironmentVariables.SMTP_PASSWORD);
        
        envLoader.GetEnvByKey(EnvironmentVariables.SMTP_SENDER);
        envLoader.GetEnvByKey(EnvironmentVariables.SMTP_RECEIVER);
    }
}