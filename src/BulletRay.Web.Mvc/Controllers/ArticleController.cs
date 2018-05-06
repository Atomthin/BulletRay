using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulletRay.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BulletRay.Web.Mvc.Controllers
{
    public class ArticleController : BulletRayControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}