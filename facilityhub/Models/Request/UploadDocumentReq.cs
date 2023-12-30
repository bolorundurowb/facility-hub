using FacilityHub.Enums;

namespace FacilityHub.Models.Request;

public class UploadDocumentReq
{
    public DocumentType Type { get; set; }

    public IFormFile File { get; set; } = null!;
}
