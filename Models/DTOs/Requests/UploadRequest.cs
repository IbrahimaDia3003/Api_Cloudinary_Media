namespace Api_Cloudinary_Media.Models.DTOs.Requests
{
    public class UploadRequest
    {
        public IFormFile File { get; set; } = null!;
        public string? Folder { get; set; }      // organiser les fichiers "produits/images"
        public string? PublicId { get; set; }
    }
}
