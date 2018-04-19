using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BulletRay.Configuration;

namespace BulletRay.Web.Host.Startup
{
    [DependsOn(
       typeof(BulletRayWebCoreModule))]
    public class BulletRayWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BulletRayWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BulletRayWebHostModule).GetAssembly());
        }
    }
}
