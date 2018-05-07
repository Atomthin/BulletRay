using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BulletRay.Authorization.Roles;
using BulletRay.Authorization.Users;
using BulletRay.MultiTenancy;
using BulletRay.Blog;

namespace BulletRay.EntityFrameworkCore
{
    public class BulletRayDbContext : AbpZeroDbContext<Tenant, Role, User, BulletRayDbContext>
    {
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategorys { get; set; }
        /* Define a DbSet for each entity of the application */

        public BulletRayDbContext(DbContextOptions<BulletRayDbContext> options)
            : base(options)
        {
        }
    }
}
