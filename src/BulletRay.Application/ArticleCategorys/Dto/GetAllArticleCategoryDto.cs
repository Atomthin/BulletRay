using Abp.Application.Services.Dto;

namespace BulletRay.ArticleCategorys.Dto
{
    public class GetAllArticleCategoryDto : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
