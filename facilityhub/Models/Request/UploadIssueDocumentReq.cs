using FacilityHub.Enums;

namespace FacilityHub.Models.Request;

public class UploadIssueDocumentReq
{
    public DocumentType Type { get; set; }

    public IFormFile File { get; set; } = null!;
}
