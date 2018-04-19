using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Blog
{
    public class PostCategory : FullAuditedEntity<int>
    {
        public PostCategory()
        {
            CreationTime = DateTime.Now;
            IsDeleted = false;
        }
        [Required]
        public virtual string CategoryName { get; set; }
        [Required]
        public virtual string CategoryDesc { get; set; }
    }
}
