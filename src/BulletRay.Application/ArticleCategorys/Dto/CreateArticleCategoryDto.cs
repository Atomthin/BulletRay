using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.ArticleCategorys.Dto
{
    [AutoMapTo(typeof(ArticleCategory))]
    public class CreateArticleCategoryDto : EntityDto<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public bool IsOpenShown { get; set; }
    }
}
