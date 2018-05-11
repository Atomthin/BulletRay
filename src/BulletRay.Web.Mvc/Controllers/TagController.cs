using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using BulletRay.Controllers;
using BulletRay.Tags;
using BulletRay.Tags.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BulletRay.Web.Mvc.Controllers
{
    [AbpMvcAuthorize]
    public class TagController : BulletRayControllerBase
    {
        private readonly ITagAppService _tagAppService;

        public TagController(ITagAppService tagAppService)
        {
            _tagAppService = tagAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [DontWrapResult]
        public async Task<JsonResult> GetData(int? draw, int start, int length, string tagKeyStr)
        {
            var pageList = await _tagAppService.GetAll(new GetAllTagDto() { SkipCount = start, MaxResultCount = length, TagKeyStr = tagKeyStr });
            return Json(pageList.Items.Select(m => new { m.Id, m.TagName }).ToList());
        }
    }
}