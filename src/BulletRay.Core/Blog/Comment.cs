using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Blog
{
    public class Comment : FullAuditedEntity<long>
    {
        public Comment()
        {
            CreationTime = DateTime.Now;
            IsDeleted = false;
        }
        [Required]
        public virtual string Content { get; set; }
        /// <summary>
        /// 评论类型 0-评论，1-回复，2-留言
        /// </summary>
        [Required]
        public virtual int Type { get; set; }
        public virtual int? Like { get; set; }
        public virtual int? UnLike { get; set; }
        public virtual string ImgUrl { get; set; }

        [Required]
        public virtual long PostId { get; set; }
        public virtual long? CommentId { get; set; }
        [Required]
        public virtual long UserId { get; set; }
    }
}
