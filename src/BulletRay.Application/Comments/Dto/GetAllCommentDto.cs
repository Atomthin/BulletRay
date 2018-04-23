using Abp.Application.Services.Dto;

namespace BulletRay.Comments.Dto
{
    public class GetAllCommentDto: PagedAndSortedResultRequestDto
    {
        public int Type { get; set; }
        public long? ArticleId { get; set; }
    }
}
