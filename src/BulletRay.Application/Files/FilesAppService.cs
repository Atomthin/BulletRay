using Abp.Application.Services;
using BulletRay.Files.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System.IO;
using System.Threading.Tasks;

namespace BulletRay.Files
{
    public class FilesAppService : IApplicationService, IFilesAppService
    {
        private static string AccessKey { get; set; }
        private static string SecretKey { get; set; }
        private IConfiguration Configuration { get; }
        public FilesAppService(IConfiguration configuration)
        {
            Configuration = configuration;
            AccessKey = Configuration["QiNiu:AccessKey"];
            SecretKey = Configuration["QiNiu:SecretKey"];
        }

        public async Task<UploadFileResult> UploadFileAsync(UploadFileDto dto)
        {
            Mac mac = new Mac(AccessKey, SecretKey);
            PutPolicy putPolicy = new PutPolicy
            {
                Scope = Configuration["QiNiu:Bucket"]
            };
            putPolicy.SetExpires(3600);
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            HttpResult result = null;
            using (var memoryStream = new MemoryStream())
            {
                var um = new UploadManager();
                await dto.File.CopyToAsync(memoryStream);
                result = await um.UploadDataAsync(memoryStream.ToArray(), $"{dto.FolderName}/{dto.File.Name}", token);
            }
            if (result != null)
            {
                return new UploadFileResult() { FileUrl = result.Text };
            }
            return new UploadFileResult();
        }
    }
}
