using CloudinaryDotNet.Actions;

namespace API;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile formFile);
    Task<DeletionResult> DeletePhotoAsync(String publicId);
}
