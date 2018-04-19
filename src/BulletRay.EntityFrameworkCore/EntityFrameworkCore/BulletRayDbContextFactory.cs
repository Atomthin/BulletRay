using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BulletRay.Configuration;
using BulletRay.Web;

namespace BulletRay.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BulletRayDbContextFactory : IDesignTimeDbContextFactory<BulletRayDbContext>
    {
        public BulletRayDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BulletRayDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            BulletRayDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BulletRayConsts.ConnectionStringName));

            return new BulletRayDbContext(builder.Options);
        }
    }
}
