using Api_Cloudinary_Media.Models.DTOs.Requests;
using Api_Cloudinary_Media.Models.DTOs.Responses;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Api_Cloudinary_Media.Models.Mapping
{
    public static class Mapper
    {
        public static RawUploadParams MapToUploadRawUploadParams(UploadRequest request, Stream stream)
        {
            return new RawUploadParams
            {
                File = new FileDescription(request.File.FileName, stream),
                Folder = request.Folder,
                PublicId = request.PublicId,
                UseFilename = true,
                UniqueFilename = string.IsNullOrEmpty(request.PublicId),
            };
        }

        public static MediaInfoResponse MapToMediaInfoResponse(GetResourceResult result, string resourceType)
        {

            return new MediaInfoResponse
            {
                PublicId = result.PublicId,
                Url = result.Url,
                SecureUrl = result.SecureUrl,
                Format = result.Format,
                ResourceType = resourceType,
                Size = result.Bytes,
                Width = result.Width,
                Height = result.Height,
                CreatedAt = result.CreatedAt
            };
        }

        public static UploadResponse MapToUploadResponse(UploadResult result, string resourceType)
        {
            return new UploadResponse
            {
                PublicId = result.PublicId,
                Url = result.Url.ToString(),
                SecureUrl = result.SecureUrl.ToString(),
                Format = result.Format,
                ResourceType = resourceType,
                Size = result.Bytes
            };
        }
        public static ImageUploadParams MapToImageUploadParams(RawUploadParams uploadParams)
        {
            return new ImageUploadParams
            {
                File = uploadParams.File,
                Folder = uploadParams.Folder,
                PublicId = uploadParams.PublicId,
                UseFilename = uploadParams.UseFilename,
                UniqueFilename = uploadParams.UniqueFilename,
            };
        }
        public static VideoUploadParams MapToVideoUploadParams(RawUploadParams uploadParams)
        {
            return new VideoUploadParams
            {
                File = uploadParams.File,
                Folder = uploadParams.Folder,
                PublicId = uploadParams.PublicId,
                UseFilename = uploadParams.UseFilename,
                UniqueFilename = uploadParams.UniqueFilename,
            };
        }
    }
}
