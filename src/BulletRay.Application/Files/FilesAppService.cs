using Abp.Application.Services;
using BulletRay.Files.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.RS;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BulletRay.Files
{
    public class FilesAppService : IApplicationService, IFilesAppService
    {
        private static string AccessKey { get; set; }
        private static string SecretKey { get; set; }
        private static string OutUrlPerfix { get; set; }
        private static string Bucket { get; set; }
        private IConfiguration Configuration { get; }

        public FilesAppService(IConfiguration configuration)
        {
            Configuration = configuration;
            AccessKey = Configuration["QiNiu:AccessKey"];
            SecretKey = Configuration["QiNiu:SecretKey"];
            Bucket = Configuration["QiNiu:Bucket"];
            OutUrlPerfix = Configuration["QiNiu:OutUrlPerfix"];
        }

        public async Task<UploadFileResult> UploadFileAsync(IFormFile file)
        {
            Mac mac = new Mac(AccessKey, SecretKey);
            PutPolicy putPolicy = new PutPolicy
            {
                Scope = Bucket
            };
            putPolicy.SetExpires(3600);
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            var list = new List<UploadFileResult>();
            HttpResult result = null;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var um = new UploadManager();
                    await file.CopyToAsync(memoryStream);
                    result = await um.UploadDataAsync(memoryStream.ToArray(), $"Article/Image/{file.FileName}", token);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            if (result != null)
            {
                var jObject = JObject.Parse(result.Text);
                return new UploadFileResult
                {
                    Name = file.FileName,
                    Url = $"{OutUrlPerfix}{jObject["key"]}"
                };
            }
            return null;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            Mac mac = new Mac(AccessKey, SecretKey);
            BucketManager bucketManager = new BucketManager(mac);
            HttpResult deleteRet = await bucketManager.DeleteAsync(Bucket, fileName);
            if (deleteRet.Code != (int)HttpCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}
