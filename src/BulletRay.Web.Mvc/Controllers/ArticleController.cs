using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using BulletRay.ArticleCategorys;
using BulletRay.Articles;
using BulletRay.Articles.Dto;
using BulletRay.Controllers;
using BulletRay.Web.Models.Article;
using BulletRay.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BulletRay.Web.Mvc.Controllers
{
    [AbpMvcAuthorize]
    public class ArticleController : BulletRayControllerBase
    {
        private readonly IArticleAppService _articleAppService;
        private readonly IArticleCategoryAppService _articleCategoryAppService;
        public ArticleController(IArticleAppService articleAppService, IArticleCategoryAppService articleCategoryAppService)
        {
            _articleAppService = articleAppService;
            _articleCategoryAppService = articleCategoryAppService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [DontWrapResult]
        public async Task<JsonResult> GetDatas([FromBody]ArticleQuery query)
        {
            var list = await _articleAppService.GetAll(new GetAllArticleDto
            {
                UserName = query.UserName,
                CategoryId = query.CategoryId,
                Title = query.Title,
                SkipCount = query.Start,
                MaxResultCount = query.Length,
                Sorting = $"{query.OrderBy},{query.OrderDir}"
            });
            return Json(new DataTableResultModel<ArticleDto>(query.Draw, list.TotalCount, list.TotalCount,
                list.Items));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}