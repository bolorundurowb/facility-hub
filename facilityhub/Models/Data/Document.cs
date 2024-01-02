using System.ComponentModel.DataAnnotations;
using FacilityHub.Enums;

namespace FacilityHub.Models.Data;

public class Document : Entity
{
    [StringLength(256)]
    public string ExternalId { get; private set; }
    
    public DocumentType Type { get; private set; }

    [StringLength(256)]
    public string FileName { get; private set; }

    public long FileSize { get; private set; }

    [StringLength(100)]
    public string MimeType { get; private set; }

    [StringLength(1024)]
    public string Url { get; private set; }

    public User? CreatedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

#pragma warning disable CS8618
    private Document() { }
#pragma warning restore CS8618

    public Document(string fileName, DocumentType documentType, long fileSize, string externalId, string url, string mimeType, User? createdBy = null)
    {
        ExternalId = externalId;
        FileName = fileName;
        FileSize = fileSize;
        Url = url;
        MimeType = mimeType;
        Type = documentType;

        CreatedBy = createdBy;
        CreatedAt = DateTimeOffset.Now;
    }
}
