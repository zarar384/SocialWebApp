using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace SocialWebAPI;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);

        _cloudinary = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uplResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var steam = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, steam),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "da-net7"
            };

            uplResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uplResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var delParams = new DeletionParams(publicId);

        return await _cloudinary.DestroyAsync(delParams);
    }
}
