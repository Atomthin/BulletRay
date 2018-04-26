using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using BulletRay.ArticleCategorys.Dto;
using BulletRay.Blog;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace BulletRay.ArticleCategorys
{
    public class ArticleCategoryAppService : AsyncCrudAppService<ArticleCategory, ArticleCategoryDto, int, GetAllArticleCategoryDto, CreateArticleCategoryDto, UpdateArticleCategoryDto>, IArticleCategoryAppService
    {
        public ArticleCategoryAppService(IRepository<ArticleCategory, int> articleCategoryRepostory) : base(articleCategoryRepostory)
        {
        }

        /// <summary>
        /// 重写GetAll方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<ArticleCategoryDto>> GetAll(GetAllArticleCategoryDto input)
        {
            var orderExpression = string.Format("{0} {1}", input.Sorting.Split(',')[0], input.Sorting.Split(',')[1]);
            var query = CreateFilteredQuery(input).WhereIf(!string.IsNullOrEmpty(input.Name),
                    m => m.Name.Contains(input.Name))
                .WhereIf(!string.IsNullOrEmpty(input.Desc), m => m.Desc.Contains(input.Desc)).OrderBy(orderExpression);
            var pagedList = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new PagedResultDto<ArticleCategoryDto>(query.Count(), pagedList.MapTo<List<ArticleCategoryDto>>());
        }
    }
}
