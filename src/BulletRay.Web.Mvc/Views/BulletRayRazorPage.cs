using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace BulletRay.Web.Views
{
    public abstract class BulletRayRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected BulletRayRazorPage()
        {
            LocalizationSourceName = BulletRayConsts.LocalizationSourceName;
        }
    }
}
