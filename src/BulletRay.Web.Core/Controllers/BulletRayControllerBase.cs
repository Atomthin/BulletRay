using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BulletRay.Controllers
{
    public abstract class BulletRayControllerBase: AbpController
    {
        protected BulletRayControllerBase()
        {
            LocalizationSourceName = BulletRayConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
