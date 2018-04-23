using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;

namespace BulletRay.Comments.Dto
{
    [AutoMap(typeof(Comment))]
    public class CommentDto : FullAuditedEntityDto<long>
    {
        public string Content { get; set; }
        public int Type { get; set; }
        public int? Like { get; set; }
        public int? UnLike { get; set; }
        public long? ArticleId { get; set; }
        public long? CommentId { get; set; }
        public long UserId { get; set; }
    }
}
