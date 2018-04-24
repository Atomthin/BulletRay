using Abp.Application.Services.Dto;

namespace BulletRay.ArticleCategorys.Dto
{
    public class GetAllArticleCategoryDto : PagedAndSortedResultRequestDto
    {
        public bool IdOpenShown { get; set; }
    }
}
