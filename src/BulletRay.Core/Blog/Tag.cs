using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Blog
{
    public class Tag : CreationAuditedEntity<int>
    {
        public Tag()
        {
            CreationTime = DateTime.Now;
        }
        [Required]
        public virtual string TagName { get; set; }
    }
}
