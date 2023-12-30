namespace FacilityHub.Services.Interfaces;

public interface IUploadResult
{
    string Id { get; }

    string Url { get; }

    long Size { get; }

    string Format { get; }

    string MimeType { get; }

    string FileName { get; }
}
