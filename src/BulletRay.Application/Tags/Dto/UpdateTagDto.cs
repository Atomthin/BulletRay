using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;

namespace BulletRay.Tags.Dto
{
    [AutoMapTo(typeof(Tag))]
    public class UpdateTagDto : EntityDto<int>
    {
        public string TagName { get; set; }
        public bool IsActive { get; set; }
    }
}
