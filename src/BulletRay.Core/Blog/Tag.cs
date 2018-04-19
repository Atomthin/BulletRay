using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulletRay.Blog
{
    public class Tag : FullAuditedEntity<int>, IPassivable
    {
        public Tag()
        {
            CreationTime = DateTime.Now;
            IsDeleted = false;
            IsActive = true;
        }
        [Required]
        public virtual string TagName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
