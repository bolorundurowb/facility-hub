using CloudinaryDotNet.Actions;
using FacilityHub.Services.Interfaces;

namespace FacilityHub.Models.Service;

public class CloudinaryResult : IUploadResult
{
    public string Id { get; }

    public string Url { get; }

    public long Size { get; }

    public string Format { get; }

    public string MimeType { get; }

    public string FileName { get; }

    public CloudinaryResult(string mimeType, RawUploadResult result)
    {
        Id = result.PublicId;
        Url = result.SecureUrl.AbsoluteUri;
        Format = result.Format;
        MimeType = mimeType;
        Size = result.Bytes;
        FileName = $"{result.OriginalFilename}.{result.Format}";
    }
}
