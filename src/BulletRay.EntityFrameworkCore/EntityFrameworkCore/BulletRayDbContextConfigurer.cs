using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BulletRay.EntityFrameworkCore
{
    public static class BulletRayDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BulletRayDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BulletRayDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
