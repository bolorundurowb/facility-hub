using FacilityHub.Enums;

namespace FacilityHub.Models.Response;

public record DocumentRes(
    Guid Id,
    DocumentType Type,
    string FileName,
    string MimeType,
    string Url,
    long FileSize,
    string? CreatedBy,
    DateTimeOffset CreatedAt
);
