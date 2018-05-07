using Abp.AutoMapper;
using Abp.Web.Models;
using BulletRay.ArticleCategorys;
using BulletRay.Articles;
using BulletRay.Articles.Dto;
using BulletRay.Controllers;
using BulletRay.Export;
using BulletRay.Web.Models.Article;
using BulletRay.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulletRay.Web.Mvc.Controllers
{
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

        [HttpGet]
        [DontWrapResult]
        public async Task<JsonResult> GetSelectData()
        {
            var dto = await _articleCategoryAppService.GetArticleCategoryIdList();
            return Json(dto);
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
    }
}