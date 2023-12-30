using System.Net;
using System.Net.Mail;
using StockQuoteAlert.Constants;
using StockQuoteAlert.Utility;

namespace StockQuoteAlert.Business;

public class Sender
{
    private string? _smtpServer;
    private int _smtpPort;

    private string? _smtpUser;
    private string? _smtpPassword;

    private string? _senderEmail;
    private string? _receiverEmail;
    
    public Sender()
    {
        ReadEnv();
    }

    public virtual void SendEmail(string subject, string body)
    {
        SmtpClient smtpClient = new SmtpClient(_smtpServer);
        
        smtpClient.Port = _smtpPort;
        smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
        smtpClient.EnableSsl = true;
        
        MailMessage mailMessage = new MailMessage(_senderEmail ?? string.Empty, _receiverEmail ?? string.Empty, subject, body);
        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"Error while sending email: {e.Message}");
        }
    }

    protected virtual void ReadEnv()
    {
        var envLoader = new DotEnvLoader();
        
        _smtpServer = envLoader.GetEnvByKey(EnvironmentVariables.SMTP_SERVER);
        _smtpPort = int.Parse(envLoader.GetEnvByKey(EnvironmentVariables.SMTP_PORT));
        
        _smtpUser = envLoader.GetEnvByKey(EnvironmentVariables.SMTP_USER);
        _smtpPassword = envLoader.GetEnvByKey(EnvironmentVariables.SMTP_PASSWORD);
        
        _senderEmail = envLoader.GetEnvByKey(EnvironmentVariables.SMTP_SENDER);
        _receiverEmail = envLoader.GetEnvByKey(EnvironmentVariables.SMTP_RECEIVER);
    }
}