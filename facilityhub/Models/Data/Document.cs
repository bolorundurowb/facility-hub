using FacilityHub.Enums;

namespace FacilityHub.Models.Data;

public class Document : Entity
{
    public DocumentType Type { get; private set; }

    public string FileName { get; private set; }

    public long FileSize { get; private set; }

    public string FileExt { get; private set; }

    public string MimeType { get; private set; }

    public string Url { get; private set; }

    public User? CreatedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

#pragma warning disable CS8618
    private Document() { }
#pragma warning restore CS8618

    public Document(User createdBy, string fileName, long fileSize, string url, string fileExt, string mimeType)
    {
        FileName = fileName;
        FileSize = fileSize;
        Url = url;
        FileExt = fileExt;
        MimeType = mimeType;

        CreatedBy = createdBy;
        CreatedAt = DateTimeOffset.Now;
    }
}
