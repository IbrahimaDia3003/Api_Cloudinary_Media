namespace Api_Cloudinary_Media.Models.DTOs.Responses
{
    public class DeleteResponse
    {
        public string PublicId { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
