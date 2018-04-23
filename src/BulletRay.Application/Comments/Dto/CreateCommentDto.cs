using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BulletRay.Blog;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Comments.Dto
{
    [AutoMapTo(typeof(Comment))]
    public class CreateCommentDto : EntityDto<long>
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int Type { get; set; }
        public long? ArticleId { get; set; }
        public long? CommentId { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
