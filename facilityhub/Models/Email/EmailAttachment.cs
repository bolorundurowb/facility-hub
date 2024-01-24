namespace FacilityHub.Models.Email;

public class EmailAttachment
{
    public byte[] Content { get; }

    public string Name { get; }

    public string MimeType { get; }

    public EmailAttachment(string name, string mimeType, byte[] content)
    {
        Name = name;
        MimeType = mimeType;
        Content = content;
    }
}