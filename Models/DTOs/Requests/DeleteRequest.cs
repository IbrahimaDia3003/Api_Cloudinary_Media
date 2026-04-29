namespace Api_Cloudinary_Media.Models.DTOs.Requests
{
    public class DeleteRequest
    {
        public string PublicId { get; set; } = string.Empty;
        public string ResourceType { get; set; } = "image"; // image | video | raw
    }
}
