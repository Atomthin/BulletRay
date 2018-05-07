using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;

namespace BulletRay.Articles.Dto
{
    [AutoMap(typeof(Article))]
    public class ArticleDto : FullAuditedEntityDto<long>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }
        public string CoverImgUrl { get; set; }
        public int? Like { get; set; }
        public int? UnLike { get; set; }
        public int? Tag { get; set; }
        public long UserId { get; set; }
        public bool IsTop { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ReadCount { get; set; }
    }
}
