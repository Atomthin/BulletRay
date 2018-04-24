using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Tags.Dto
{
    [AutoMapTo(typeof(Tag))]
    public class CreateTagDto : EntityDto<int>
    {
        [Required]
        public string TagName { get; set; }
    }
}
