using dotenv.net.Utilities;
using FacilityHub.Models.Email;
using FacilityHub.Services.Interfaces;
using FluentEmail.Core;
using FluentEmail.Mailgun;

namespace FacilityHub.Services.Implementations;

public class MailgunEmailService : IEmailService
{
    private readonly ILogger<MailgunEmailService> _logger;
    private readonly string _serviceEmail;

    public MailgunEmailService(ILogger<MailgunEmailService> logger)
    {
        EnvReader.TryGetStringValue("MAILGUN_DOMAIN", out var domain);
        EnvReader.TryGetStringValue("MAILGUN_API_KEY", out var apiKey);
        EnvReader.TryGetStringValue("SERVICE_EMAIL", out _serviceEmail);

        _logger = logger;
        var sender = new MailgunSender(domain, apiKey);
        Email.DefaultSender = sender;
    }

    public async Task<bool> SendAsync(EmailRecipient recipient, EmailMessage emailMessage)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("FacilityHub", _serviceEmail));
            message.To.Add(new MailboxAddress(recipient.Name, recipient.Email));
            message.Subject = emailMessage.Subject;


            if (emailMessage.Attachments.Any())
            {
                foreach (var attachment in emailMessage.Attachments)
                {
                    var attachmentStream = new MemoryStream(attachment.Content);
                    attachmentStream.Position = 0;
                    var mimePart = new MimePart(attachment.MimeType)
                    {
                        Content = new MimeContent(attachmentStream, ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = attachment.Name
                    };
                    message.Body = new Multipart("mixed")
                    {
                        mimePart,
                        message.Body
                    };
                }
            }
            else
            {
                message.Body = new TextPart("html")
                {
                    Text = emailMessage.Content
                };
            }

            await _client.AuthenticateAsync(_mailUsername, _mailPassword);
            var result = await _client.SendAsync(message);

            var emailSent = false;

            _client.MessageSent += (sender, args) =>
            {
                _logger.LogInformation("Email sent successfully");
                emailSent = true;
            };

            return emailSent;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending email");
            return false;
        }
        finally
        {
            await _client.DisconnectAsync(true);
        }
    }
}