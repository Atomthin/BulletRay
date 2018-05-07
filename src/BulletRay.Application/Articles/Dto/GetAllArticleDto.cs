using Abp.Application.Services.Dto;

namespace BulletRay.Articles.Dto
{
    public class GetAllArticleDto : PagedAndSortedResultRequestDto
    {
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
    }
}
