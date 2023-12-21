using CloudinaryDotNet.Actions;
using FacilityHub.Services.Interfaces;

namespace FacilityHub.Models.Service;

public class CloudinaryResult : IUploadResult
{
    public string Id { get; }
    public string Url { get; }

    public CloudinaryResult(UploadResult result)
    {
        Id = result.PublicId;
        Url = result.SecureUrl.AbsoluteUri;
    }
}
