using dotenv.net.Utilities;

namespace FacilityHub.Models.Email;

public class EmailMessage
{
    public string Subject { get; }

    public string Content { get; }

    public IEnumerable<EmailAttachment> Attachments { get; }

    public string Sender { get; }

    public string? ReplyTo { get; }

    public EmailMessage(string subject, string content, IEnumerable<EmailAttachment>? attachments = null,
        string? sender = null,
        string? replyTo = null)
    {
        Subject = subject;
        Content = content;
        Attachments = attachments ?? Enumerable.Empty<EmailAttachment>();
        Sender = sender ?? EnvReader.GetStringValue("SERVICE_EMAIL");
        ReplyTo = replyTo;
    }
}
