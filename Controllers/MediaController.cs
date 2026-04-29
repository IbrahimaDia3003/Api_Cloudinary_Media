using Api_Cloudinary_Media.Models.DTOs.Requests;
using Api_Cloudinary_Media.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api_Cloudinary_Media.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class MediaController(ICloudinaryService cloudinaryService) : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;


        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest(new { message = "Aucun fichier fourni." });

            var result = await _cloudinaryService.UploadAsync(request);
            return Ok(result);
        }

        [HttpGet("info/{resourceType}/{**publicId}")]
        public async Task<IActionResult> GetMedia(string resourceType, string publicId)
        {
            var decodedPublicId = Uri.UnescapeDataString(publicId);

            if (string.IsNullOrWhiteSpace(decodedPublicId))
                return BadRequest(new { message = "Le PublicId est obligatoire." });

            var result = await _cloudinaryService.GetAsync(decodedPublicId, resourceType);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.PublicId))
                return BadRequest(new { message = "Le PublicId est obligatoire." });

            var result = await _cloudinaryService.DeleteAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
