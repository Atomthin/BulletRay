using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Validation;
using BulletRay.ArticleCategorys.Dto;
using BulletRay.Blog;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BulletRay.ArticleCategorys
{
    public class ArticleCategoryAppService : AsyncCrudAppService<ArticleCategory, ArticleCategoryDto, int, GetAllArticleCategoryDto, CreateArticleCategoryDto, UpdateArticleCategoryDto>, IArticleCategoryAppService
    {
        private readonly IRepository<Article, long> _articleRepository;
        public ArticleCategoryAppService(IRepository<ArticleCategory, int> articleCategoryRepostory, IRepository<Article, long> articleRepository) : base(articleCategoryRepostory)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 重写GetAll方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisableValidation]
        public override async Task<PagedResultDto<ArticleCategoryDto>> GetAll(GetAllArticleCategoryDto input)
        {
            var orderExpression = !string.IsNullOrEmpty(input.Sorting) ? string.Format("{0} {1}", input.Sorting.Split(',')[0], input.Sorting.Split(',')[1]) : string.Empty;
            var query = CreateFilteredQuery(input).WhereIf(!string.IsNullOrEmpty(input.Name),
                    m => m.Name.Contains(input.Name))
                .WhereIf(!string.IsNullOrEmpty(input.Desc), m => m.Desc.Contains(input.Desc));
            if (!string.IsNullOrEmpty(orderExpression))
            {
                query = query.OrderBy(orderExpression);
            }
            var pagedList = new List<ArticleCategory>();
            if (input.SkipCount == 0 && input.MaxResultCount == 0)
            {
                pagedList = await query.ToListAsync();
            }
            else
            {
                pagedList = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            }
            return new PagedResultDto<ArticleCategoryDto>(query.Count(), pagedList.MapTo<List<ArticleCategoryDto>>());
        }

        public async Task<List<ArticleCategoryIdDto>> GetArticleCategoryIdList(long? articleId, bool isOpenShown = false)
        {
            var query = Repository.GetAll().Where(m => m.IsOpenShown == isOpenShown);
            if (isOpenShown)
            {
                query = Repository.GetAll();
            }
            var list = await query.Select(m => new ArticleCategoryIdDto { Id = m.Id, Name = m.Name, Desc = m.Desc, IsSelected = false }).ToListAsync();
            if (articleId != null)
            {
                var articleInfo = await _articleRepository.FirstOrDefaultAsync(m => m.Id == articleId);
                if (articleInfo != null)
                {
                    list.FirstOrDefault(m => m.Id == articleInfo.CategoryId).IsSelected = true;
                }
            }
            return list;
        }
    }
}
