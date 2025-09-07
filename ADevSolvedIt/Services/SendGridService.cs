using ADevSolvedIt.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ADevSolvedIt.Services;

public class SendGridService : ISendGridService
{
    private readonly IConfiguration _configuration;

    public SendGridService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string emailBody, string subject)
    {
        var apiKey = _configuration["SendGridApiKey"];

        var client = new SendGridClient(apiKey);

        var message = new SendGridMessage
        {
            From = new EmailAddress("dandumitru27@gmail.com", "Dan Dumitru"),
            Subject = subject,
            HtmlContent = emailBody
        };

        message.AddTo(new EmailAddress("dandumitru27@gmail.com", "Dan Dumitru"));

        await client.SendEmailAsync(message);
    }
}
