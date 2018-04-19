using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using BulletRay.Controllers;

namespace BulletRay.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : BulletRayControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
