namespace Api_Cloudinary_Media.Models.DTOs.Responses
{
    public class MediaInfoResponse
    {
        public string PublicId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string SecureUrl { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string ResourceType { get; set; } = string.Empty;
        public long Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string CreatedAt { get; set; } =string.Empty;
    }
}
