namespace FacilityHub.Services.Interfaces;

public interface IMediaHandlerService
{
    Task<IUploadResult?> UploadAsync(string fileName, Stream stream);

    Task DeleteAsync(string id);
}
