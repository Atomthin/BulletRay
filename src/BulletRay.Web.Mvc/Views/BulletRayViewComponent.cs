using Abp.AspNetCore.Mvc.ViewComponents;

namespace BulletRay.Web.Views
{
    public abstract class BulletRayViewComponent : AbpViewComponent
    {
        protected BulletRayViewComponent()
        {
            LocalizationSourceName = BulletRayConsts.LocalizationSourceName;
        }
    }
}
