namespace Api_Cloudinary_Media.Models.DTOs.Responses
{
    public class UploadResponse
    {
        public string PublicId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string SecureUrl { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string ResourceType { get; set; } = string.Empty; // image, video, raw
        public long Size { get; set; }
    }
}
