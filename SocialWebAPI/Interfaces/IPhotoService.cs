using CloudinaryDotNet.Actions;

namespace SocialWebAPI;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
