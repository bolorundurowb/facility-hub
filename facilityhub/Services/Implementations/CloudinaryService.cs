using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net.Utilities;
using FacilityHub.Models.Service;
using FacilityHub.Services.Interfaces;

namespace FacilityHub.Services.Implementations;

public class CloudinaryService : IMediaHandlerService
{
    private ILogger<CloudinaryService> _logger;
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(ILogger<CloudinaryService> logger)
    {
        EnvReader.TryGetStringValue("CLOUDINARY_URL", out var cloudinaryUrl);

        _logger = logger;
        _cloudinary = new Cloudinary(cloudinaryUrl);
    }

    public async Task<IUploadResult?> UploadAsync(string fileName, Stream stream)
    {
        try
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return new CloudinaryResult(uploadResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Image upload failed");
            return null;
        }
    }

    public async Task DeleteAsync(string id)
    {
        var deleteParam = new DeletionParams(id);
        await _cloudinary.DestroyAsync(deleteParam);
    }
}
