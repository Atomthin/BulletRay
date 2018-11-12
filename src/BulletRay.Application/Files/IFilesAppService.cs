using BulletRay.Files.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulletRay.Files
{
    public interface IFilesAppService
    {
        Task<UploadFileResult> UploadFileAsync(UploadFileDto dto);
    }
}
