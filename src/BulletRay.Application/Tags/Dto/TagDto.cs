using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;

namespace BulletRay.Tags.Dto
{
    [AutoMap(typeof(Tag))]
    public class TagDto : FullAuditedEntityDto<int>
    {
        public string TagName { get; set; }
        public bool IsActive { get; set; }
    }
}
