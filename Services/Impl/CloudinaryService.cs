using Api_Cloudinary_Media.Config;
using Api_Cloudinary_Media.Models.DTOs.Requests;
using Api_Cloudinary_Media.Models.DTOs.Responses;
using Api_Cloudinary_Media.Models.Mapping;
using Api_Cloudinary_Media.Services.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace Api_Cloudinary_Media  .Services.Impl
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public readonly Configuration? configuration;
        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var settings = config.Value;

            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true; // Force HTTPS
        }

        public async Task<UploadResponse> UploadAsync(UploadRequest request)
        {
            await using var stream = request.File.OpenReadStream();

            // Déterminer le type de ressource selon le MIME
            var resourceType = GetResourceType(request.File.ContentType);

            var uploadParams = Mapper.MapToUploadRawUploadParams(request, stream);

            // Choisir le bon type d'upload selon la ressource
            UploadResult result = resourceType switch
            {
                "video" => await _cloudinary.UploadAsync(Mapper.MapToVideoUploadParams(uploadParams)),
                "image" => await _cloudinary.UploadAsync(Mapper.MapToImageUploadParams(uploadParams)),
                _ => await _cloudinary.UploadAsync(uploadParams) // raw pour documents
            };

            if (result.Error != null)
                throw new Exception($"Cloudinary upload error : {result.Error.Message}");   

            return Mapper.MapToUploadResponse(result, resourceType);
        }

        public async Task<MediaInfoResponse> GetAsync(string publicId, string resourceType)
        {
            var type = resourceType.ToLower() switch
            {
                "video" => ResourceType.Video,
                "image" => ResourceType.Image,
                _       => ResourceType.Raw
            };

            var result = await _cloudinary.GetResourceAsync(new GetResourceParams(publicId)
                        {
                            ResourceType = type
                        });

            if (result.Error != null)
                throw new Exception($"Cloudinary get resource error : {result.Error.Message}");

            return Mapper.MapToMediaInfoResponse(result, resourceType);
        }


        public async Task<DeleteResponse> DeleteAsync(DeleteRequest request)
        {
            var resourceType = request.ResourceType.ToLower() switch
            {
                "video" => ResourceType.Video,
                "image" => ResourceType.Image,
                _ => ResourceType.Raw
            };

            var deleteParams = new DeletionParams(request.PublicId)
            {
                ResourceType = resourceType
            };

            var result = await _cloudinary.DestroyAsync(deleteParams);

            var success = result.Result == "ok";

            return new DeleteResponse
            {
                PublicId = request.PublicId,
                Success = success,
                Message = success ? "Fichier supprimé avec succès" : $"Échec : {result.Error?.Message}"
            };
        }

        private static string GetResourceType(string contentType) => contentType switch
        {
            var ct when ct.StartsWith("image/") => "image",
            var ct when ct.StartsWith("video/") => "video",
            _ => "raw"   // PDF, Word, etc.
        };

    }
}
