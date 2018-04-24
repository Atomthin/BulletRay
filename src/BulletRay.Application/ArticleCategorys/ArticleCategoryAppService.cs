using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using BulletRay.ArticleCategorys.Dto;
using BulletRay.Blog;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulletRay.ArticleCategorys
{
    public class ArticleCategoryAppService : AsyncCrudAppService<ArticleCategory, ArticleCategoryDto, int, GetAllArticleCategoryDto, CreateArticleCategoryDto, UpdateArticleCategoryDto>, IArticleCategoryAppService
    {
        public ArticleCategoryAppService(IRepository<ArticleCategory> articleCategoryRepostory) : base(articleCategoryRepostory)
        {
        }

        /// <summary>
        /// 重写GetAll方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<ArticleCategoryDto>> GetAll(GetAllArticleCategoryDto input)
        {
            var query = base.CreateFilteredQuery(input).Where(m => m.IsOpenShown == input.IdOpenShown);
            var pagedList = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new PagedResultDto<ArticleCategoryDto>(query.Count(), pagedList.MapTo<List<ArticleCategoryDto>>());
        }
    }
}
