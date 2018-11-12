using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Blog
{
    public class Article : FullAuditedEntity<long>
    {
        public Article()
        {
            CreationTime = DateTime.Now;
            IsDeleted = false;
            IsTop = false;
            ReadCount = 0;
        }
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual string Content { get; set; }
        public virtual string ShortDesc { get; set; }
        public virtual string CoverImgUrl { get; set; }
        public virtual int? Like { get; set; }
        public virtual int? UnLike { get; set; }
        public virtual long? TagNum { get; set; }
        public virtual string TagStr { get; set; }
        [Required]
        public virtual bool IsTop { get; set; }
        [Required]
        public virtual int CategoryId { get; set; }
        public virtual int ReadCount { get; set; }
    }
}
