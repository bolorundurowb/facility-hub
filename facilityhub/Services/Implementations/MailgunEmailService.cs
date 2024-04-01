using dotenv.net.Utilities;
using FacilityHub.Models.Email;
using FacilityHub.Services.Interfaces;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Mailgun;

namespace FacilityHub.Services.Implementations;

public class MailgunEmailService : IEmailService
{
    private readonly ILogger<MailgunEmailService> _logger;

    public MailgunEmailService(ILogger<MailgunEmailService> logger)
    {
        EnvReader.TryGetStringValue("MAILGUN_DOMAIN", out var domain);
        EnvReader.TryGetStringValue("MAILGUN_API_KEY", out var apiKey);

        _logger = logger;
        var sender = new MailgunSender(domain, apiKey);
        Email.DefaultSender = sender;
    }

    public async Task<bool> SendAsync(EmailRecipient recipient, EmailMessage emailMessage)
    {
        try
        {
            var attachments = emailMessage.Attachments
                .Select(x => new Attachment());
            var message = Email
                .From(emailMessage.Sender)
                .To(recipient.Email, recipient.Name)
                .ReplyTo(emailMessage.ReplyTo)
                .Subject(emailMessage.Subject)
                .Body(emailMessage.Content)
                .Attach(attachments);

            var response = await message.SendAsync();

            if (response?.Successful == true)
            {
                _logger.LogInformation("Email with {Subject} successfully sent to '{Email}'", emailMessage.Subject,
                    recipient.Email);
                return true;
            }

            _logger.LogError("Email with {Subject} failed to be sent to '{Email}'. Errors: {Errors}",
                emailMessage.Subject, recipient.Email,
                string.Join(",", response?.ErrorMessages ?? new List<string?>()));
            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending email");
            return false;
        }
    }
}
