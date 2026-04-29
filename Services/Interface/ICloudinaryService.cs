using Api_Cloudinary_Media.Models.DTOs.Requests;
using Api_Cloudinary_Media.Models.DTOs.Responses;

namespace Api_Cloudinary_Media.Services.Interface
{
    public interface ICloudinaryService
    {
        Task<UploadResponse> UploadAsync(UploadRequest request);
        Task<MediaInfoResponse> GetAsync(string publicId, string resourceType);
        Task<DeleteResponse> DeleteAsync(DeleteRequest request);
    }
}
