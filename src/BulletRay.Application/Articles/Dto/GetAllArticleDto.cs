using Abp.Application.Services.Dto;

namespace BulletRay.Articles.Dto
{
    public class GetAllArticleDto : PagedAndSortedResultRequestDto
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public bool IsAsc { get; set; } = false;
    }
}
