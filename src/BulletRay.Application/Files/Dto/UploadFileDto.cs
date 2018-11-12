using Microsoft.AspNetCore.Http;

namespace BulletRay.Files.Dto
{
    public class UploadFileDto
    {
        public IFormFile File { get; set; }
        public string FolderName { get; set; }
    }
}
