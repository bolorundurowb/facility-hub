using dotenv.net.Utilities;
using FacilityHub.Models.Email;
using FacilityHub.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FacilityHub.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly SmtpClient _client;
    private readonly string _serviceEmail;
    private readonly string _mailUsername;
    private readonly string _mailPassword;

    public EmailService(ILogger<EmailService> logger)
    {
        EnvReader.TryGetStringValue("MAIL_HOST", out var mailHost);
        EnvReader.TryGetStringValue("MAIL_PORT", out var mailPort);
        _mailUsername = EnvReader.GetStringValue("MAIL_USERNAME");
        _mailPassword = EnvReader.GetStringValue("MAIL_PASSWORD");
        _serviceEmail = EnvReader.GetStringValue("SERVICE_EMAIL");

        _logger = logger;
        _client = new SmtpClient();
        _logger.LogInformation("Connecting to mail server on {MailHost}:{MailPort}", mailHost, mailPort);
        _client.Connect(mailHost, int.Parse(mailPort), SecureSocketOptions.StartTls);
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