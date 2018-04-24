using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;

namespace BulletRay.ArticleCategorys.Dto
{
    [AutoMap(typeof(ArticleCategory))]
    public class ArticleCategoryDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsOpenShown { get; set; }
    }
}
