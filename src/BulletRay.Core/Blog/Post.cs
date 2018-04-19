using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Blog
{
    public class Post : FullAuditedEntity<long>
    {
        public Post()
        {
            CreationTime = DateTime.Now;
            IsDeleted = false;
            IsTop = false;
        }
        [Required]
        public virtual string PostName { get; set; }
        [Required]
        public virtual string PostContent { get; set; }
        public virtual string ShortDesc { get; set; }
        public virtual int? Like { get; set; }
        public virtual int? Tag { get; set; }
        [Required]
        public virtual long UserId { get; set; }
        [Required]
        public virtual bool IsTop { get; set; }
        [Required]
        public virtual int CategoryId { get; set; }
    }
}
