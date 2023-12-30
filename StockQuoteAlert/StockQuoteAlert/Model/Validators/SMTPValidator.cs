using System.Net.Sockets;
using StockQuoteAlert.Constants;
using StockQuoteAlert.Exceptions;
using StockQuoteAlert.Utility;

namespace StockQuoteAlert.Model.Validators;

public class SMTPValidator : IValidator
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    
    public SMTPValidator()
    {
        var envLoader = new DotEnvLoader();

        _smtpServer = envLoader.GetEnvByKey(EnvironmentVariables.SMTP_SERVER);
        _smtpPort = int.Parse(envLoader.GetEnvByKey(EnvironmentVariables.SMTP_PORT));
    }

    public void Validate()
    {
        CheckSmtpServer();
    }
    
    private void CheckSmtpServer()
    {
        try
        {
            var client = new TcpClient();
            client.Connect(_smtpServer, _smtpPort);
            client.Close();
        }
        catch (Exception e)
        {
            throw new ValidationException(ValidationErrorCode.SMTP_CANT_CONNECT, e);
        }
    }
}