using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using BulletRay.ArticleCategorys;
using BulletRay.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BulletRay.Web.Mvc.Controllers
{
    [AbpMvcAuthorize]
    public class ArticleCategoryController : BulletRayControllerBase
    {
        private readonly IArticleCategoryAppService _articleCategoryAppService;
        public ArticleCategoryController(IArticleCategoryAppService articleCategoryAppService)
        {
            _articleCategoryAppService = articleCategoryAppService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}