using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using BulletRay.ArticleCategorys;
using BulletRay.ArticleCategorys.Dto;
using BulletRay.Controllers;
using BulletRay.Export;
using BulletRay.Web.Models.ArticleCategory;
using BulletRay.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return PartialView("_CreateArticleCategoryModal");
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

        [HttpPost]
        [DontWrapResult]
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
            var fileName = string.Format("导出文章分类-{0}.xlsx", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            var byteArr = ExcelManager.CreateExcelFileToByteArray(list.Items, new Hashtable
            {
                { "Id","文章分类编号"},
                { "Name","文章分类名字"},
                { "Desc","文章分简介"},
                { "CreationTime","文章分类创建时间"},
                { "IsOpenShown","是否开放展示"}
            }, fileName);
            var fileGuid = Guid.NewGuid().ToString();
            TempData[fileGuid] = byteArr;
            return Json(new { DownLoadUrl = Url.Action("Download", "ArticleCategory", new { fileGuid, fileName }) });
        }

        [HttpGet]
        public ActionResult Download(string fileGuid, string fileName)
        {
            var data = (byte[])TempData[fileGuid];
            return File(data, "application/ms-excel", fileName);
        }
    }
}