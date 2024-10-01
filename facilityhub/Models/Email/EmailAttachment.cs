namespace FacilityHub.Models.Email;

public class EmailAttachment(string name, string mimeType, byte[] content)
{
    public byte[] Content { get; } = content;

    public string Name { get; } = name;

    public string MimeType { get; } = mimeType;
}