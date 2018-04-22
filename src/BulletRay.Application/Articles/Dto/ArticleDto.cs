using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;

namespace BulletRay.Articles.Dto
{
    [AutoMap(typeof(Article))]
    public class ArticleDto : FullAuditedEntityDto<long>
    {
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string ShortDesc { get; set; }
        public virtual string CoverImgUrl { get; set; }
        public virtual int? Like { get; set; }
        public virtual int? UnLike { get; set; }
        public virtual int? Tag { get; set; }
        public virtual long UserId { get; set; }
        public virtual bool IsTop { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int ReadCount { get; set; }
    }
}
