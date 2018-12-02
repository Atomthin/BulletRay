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
            var query = from a in Repository.GetAll()
                        join ac in _articleCategoryRepository.GetAll() on a.CategoryId equals ac.Id
                        select new ArticleDto
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Content = a.Content,
                            ShortDesc = a.ShortDesc,
                            CoverImgUrl = a.CoverImgUrl,
                            IsTop = a.IsTop,
                            CategoryId = a.CategoryId,
                            CategoryName = ac.Name
                        };
            query = query.WhereIf(input.CategoryId != 0, m => m.CategoryId == input.CategoryId);
            var orderExpression = !string.IsNullOrEmpty(input.Sorting) ? string.Format("{0} {1}", input.Sorting.Split(',')[0], input.Sorting.Split(',')[1]) : string.Empty;
            if (!string.IsNullOrEmpty(orderExpression))
            {
                query = query.OrderBy(orderExpression);
            }
            var articleList = new List<ArticleDto>();
            if (input.SkipCount == 0 && input.MaxResultCount == 0)
            {
                articleList = await query.ToListAsync();
            }
            else
            {
                articleList = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            }
            return new PagedResultDto<ArticleDto>(query.Count(), articleList);
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
