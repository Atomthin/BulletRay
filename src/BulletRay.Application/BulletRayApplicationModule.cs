using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BulletRay.Authorization;

namespace BulletRay
{
    [DependsOn(
        typeof(BulletRayCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BulletRayApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BulletRayAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BulletRayApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
