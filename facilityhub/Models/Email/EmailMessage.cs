namespace FacilityHub.Models.Email;

public class EmailMessage
{
    public string Subject { get; }

    public string Content { get; }

    public IEnumerable<EmailAttachment> Attachments { get; }

    public string Sender { get; }

    public string? ReplyTo { get; }

    public EmailMessage(string subject, string content, IEnumerable<EmailAttachment>? attachments, string? sender,
        string? replyTo)
    {
        Subject = subject;
        Content = content;
        Attachments = attachments ?? Enumerable.Empty<EmailAttachment>();
        Sender = sender ?? "no-reply@facilityhub.africa";
        ReplyTo = replyTo;
    }
}