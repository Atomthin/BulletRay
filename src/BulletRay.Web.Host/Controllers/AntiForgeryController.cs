using Microsoft.AspNetCore.Antiforgery;
using BulletRay.Controllers;

namespace BulletRay.Web.Host.Controllers
{
    public class AntiForgeryController : BulletRayControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
