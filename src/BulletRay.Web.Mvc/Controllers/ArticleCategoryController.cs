using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using BulletRay.ArticleCategorys;
using BulletRay.ArticleCategorys.Dto;
using BulletRay.Controllers;
using BulletRay.Web.Models.ArticleCategory;
using BulletRay.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace BulletRay.Web.Controllers
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

        [HttpPost]
        [DontWrapResult]
        public async Task<IActionResult> Create(CreateArticleCategoryModel model)
        {
            var entityDto = await _articleCategoryAppService.Create(new CreateArticleCategoryDto() { Name = model.Name, Desc = model.Desc, IsOpenShown = model.IsOpenShown });
            if (entityDto != null)
            {
                return Json(new ResultBaseModel() { Result = true });
            }
            return Json(new ResultBaseModel() { Result = false });
        }

        [HttpPost]
        [DontWrapResult]
        public async Task<JsonResult> GetDatas(ArticleCategoryQuery query)
        {
            var list = await _articleCategoryAppService.GetAll(new GetAllArticleCategoryDto
            {
                Name = query.Name,
                Desc = query.Desc,
                SkipCount = query.Start,
                MaxResultCount = query.Length,
                Sorting = $"{query.OrderBy},{query.OrderDir}"
            });
            return Json(new DataTableResultModel<ArticleCategoryDto>(query.Draw, list.TotalCount, list.TotalCount,
                list.Items));
        }

    }
}