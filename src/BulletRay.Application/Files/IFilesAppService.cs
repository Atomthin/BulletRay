using BulletRay.Files.Dto;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BulletRay.Files
{
    public interface IFilesAppService
    {
        Task<UploadFileResult> UploadFileAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string fileName);
    }
}
