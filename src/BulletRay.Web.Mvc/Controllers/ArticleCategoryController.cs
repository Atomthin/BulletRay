using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using BulletRay.ArticleCategorys;
using BulletRay.ArticleCategorys.Dto;
using BulletRay.Controllers;
using BulletRay.Export;
using BulletRay.Web.Models.ArticleCategory;
using BulletRay.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateArticleCategoryModal");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int articleCategoryId)
        {
            var dto = await _articleCategoryAppService.Get(new EntityDto(articleCategoryId));
            var model = new EditArticleCategoryModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Desc = dto.Desc,
                IsOpenShown = dto.IsOpenShown
            };
            return PartialView("_EditArticleCategoryModal", model);
        }

        [HttpPost]
        [DontWrapResult]
        public async Task<JsonResult> GetDatas([FromBody]ArticleCategoryQuery query)
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

        [HttpGet]
        public async Task<IActionResult> Export(string name, string desc)
        {
            var list = await _articleCategoryAppService.GetAll(new GetAllArticleCategoryDto
            {
                Name = name,
                Desc = desc,
                SkipCount = 0,
                MaxResultCount = 0,
                Sorting = string.Empty
            });
            var byteArr = ExcelManager.CreateExcelFileToByteArray(list.Items.Select(m => new { m.Id, m.Name, m.Desc, m.CreationTime, m.IsOpenShown }), new Hashtable
            {
                { "Id","文章分类编号"},
                { "Name","文章分类名字"},
                { "Desc","文章分简介"},
                { "CreationTime","文章分类创建时间"},
                { "IsOpenShown","是否开放展示"}
            });
            return File(byteArr, "application/vnd.ms-excel");
        }
    }
}