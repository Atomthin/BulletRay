using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;

namespace BulletRay.Articles.Dto
{
    [AutoMapTo(typeof(Article))]
    public class UpdateArticleDto : EntityDto<long>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }
        public string CoverImgUrl { get; set; }
        public bool IsTop { get; set; }
        public string TagStr { get; set; }
        public long? TagNum { get; set; }
        public int CategoryId { get; set; }
    }
}
