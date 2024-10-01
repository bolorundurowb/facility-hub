using CloudinaryDotNet.Actions;
using FacilityHub.Services.Interfaces;

namespace FacilityHub.Models.Service;

public class CloudinaryResult(string mimeType, RawUploadResult result) : IUploadResult
{
    public string Id { get; } = result.PublicId;

    public string Url { get; } = result.SecureUrl.AbsoluteUri;

    public long Size { get; } = result.Bytes;

    public string Format { get; } = result.Format;

    public string MimeType { get; } = mimeType;

    public string FileName { get; } = $"{result.OriginalFilename}.{result.Format}";
}
