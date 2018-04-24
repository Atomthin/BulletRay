using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Blog
{
    public class ArticleCategory : FullAuditedEntity<int>
    {
        public ArticleCategory()
        {
            CreationTime = DateTime.Now;
            IsDeleted = false;
            IsOpenShown = false;
        }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Desc { get; set; }
        [Required]
        public virtual bool IsOpenShown { get; set; }
    }
}
