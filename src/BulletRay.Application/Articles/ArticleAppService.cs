using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using BulletRay.Articles.Dto;
using BulletRay.Authorization.Users;
using BulletRay.Blog;
using BulletRay.CommonDto;
using BulletRay.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BulletRay.Articles
{
    public class ArticleAppService : AsyncCrudAppService<Article, ArticleDto, long, GetAllArticleDto, CreateArticleDto, UpdateArticleDto>, IArticleAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<ArticleCategory, int> _articleCategoryRepository;
        public ArticleAppService(IRepository<Article, long> articleRepository, IRepository<User, long> userRepository, IRepository<ArticleCategory, int> articleCategoryRepository) : base(articleRepository)
        {
            _userRepository = userRepository;
            _articleCategoryRepository = articleCategoryRepository;
        }

        /// <summary>
        /// 重写创建方法，不对外暴露
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RemoteService(false)]
        public override async Task<ArticleDto> Create(CreateArticleDto input)
        {
            var articleInfo = MapToEntity(input);
            var resultEntity = await Repository.InsertAsync(articleInfo);
            return MapToEntityDto(resultEntity);
        }


        /// <summary>
        /// 重写GetAll方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<ArticleDto>> GetAll(GetAllArticleDto input)
        {
            var query = CreateFilteredQuery(input);
            var orderExpression = !string.IsNullOrEmpty(input.Sorting) ? string.Format("{0} {1}", input.Sorting.Split(',')[0], input.Sorting.Split(',')[1]) : string.Empty;
            if (!string.IsNullOrEmpty(orderExpression))
            {
                query = query.OrderBy(orderExpression);
            }
            var articleList = new List<Article>();
            if (input.SkipCount == 0 && input.MaxResultCount == 0)
            {
                articleList = await query.ToListAsync();
            }
            else
            {
                articleList = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            }
            var pagedList = articleList.MapTo<List<ArticleDto>>();
            var categoryIdList = pagedList.Select(m => m.Id).Distinct();
            var categoryList = _articleCategoryRepository.GetAll().Where(m => categoryIdList.Contains(m.Id));
            foreach (var item in categoryList)
            {
                pagedList.Where(m => m.CategoryId == item.Id).ToList().ForEach(m => m.CategoryName = item.Name);
            }
            return new PagedResultDto<ArticleDto>(query.Count(), pagedList);
        }

        /// <summary>
        /// 重写基类CreateFilteredQuery增加筛选条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Article> CreateFilteredQuery(GetAllArticleDto input)
        {
            var query = base.CreateFilteredQuery(input);
            if (!string.IsNullOrEmpty(input.UserName))
            {
                query = from user in _userRepository.GetAll()
                        join article in Repository.GetAll() on user.Id equals article.CreatorUserId
                        where user.UserName == input.UserName
                        select article;
            }
            query.WhereIf(!string.IsNullOrEmpty(input.Title), m => m.Title.Contains(input.Title)).WhereIf(input.CategoryId != 0, m => m.CategoryId == input.CategoryId);
            return query;
        }

        /// <summary>
        /// 更新Like和Unlike
        /// </summary>
        /// <param name="input"></param>
        public async void UpdateLikeOrUnLikeAsync(UpdataLikeOrUnLikeDto input)
        {
            var entity = await Repository.GetAll().FirstOrDefaultAsync(m => m.Id == input.Id);
            if (entity != null)
            {
                switch (input.LikeType)
                {
                    case EnumHelper.LikeType.AddLike:
                        entity.Like = entity.Like++;
                        break;
                    case EnumHelper.LikeType.SubLike:
                        entity.Like = entity.Like--;
                        break;
                    case EnumHelper.LikeType.AddUnLike:
                        entity.UnLike = entity.UnLike++;
                        break;
                    case EnumHelper.LikeType.SubUnlike:
                        entity.UnLike = entity.UnLike--;
                        break;
                }
            }
            await Repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新阅读量
        /// </summary>
        /// <param name="articleId"></param>
        public async void UpdateReadCountAsync(long articleId)
        {
            var entity = await Repository.GetAll().FirstOrDefaultAsync(m => m.Id == articleId);
            entity.ReadCount++;
            await Repository.UpdateAsync(entity);
        }
    }
}
