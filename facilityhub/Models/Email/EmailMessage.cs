using dotenv.net.Utilities;

namespace FacilityHub.Models.Email;

public class EmailMessage(
    string subject,
    string content,
    IEnumerable<EmailAttachment>? attachments = null,
    string? sender = null,
    string? replyTo = null)
{
    public string Subject { get; } = subject;

    public string Content { get; } = content;

    public IEnumerable<EmailAttachment> Attachments { get; } = attachments ?? [];

    public string Sender { get; } = sender ?? EnvReader.GetStringValue("SERVICE_EMAIL");

    public string? ReplyTo { get; } = replyTo;
}
