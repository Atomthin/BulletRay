using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using BulletRay.Articles.Dto;
using BulletRay.Authorization.Users;
using BulletRay.Blog;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using BulletRay.Utility;

namespace BulletRay.Articles
{
    public class ArticleAppService : AsyncCrudAppService<Article, ArticleDto, long, GetAllArticleDto, CreateArticleDto, UpdateArticleDto>, IArticleAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Article, long> _articleRepository;

        public ArticleAppService(IRepository<Article, long> articleRepository, IRepository<User, long> userRepository) : base(articleRepository)
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<ArticleDto> Create(CreateArticleDto input)
        {
            var entity = input.MapTo<Article>();
            entity.UserId = AbpSession.UserId.Value;
            var article = await _articleRepository.InsertAsync(entity);
            return article.MapTo<ArticleDto>();
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<ArticleDto> Update(UpdateArticleDto input)
        {
            var entity = input.MapTo<Article>();
            entity.LastModifierUserId = AbpSession.UserId.Value;
            entity.LastModificationTime = DateTime.Now;
            var article = await _articleRepository.UpdateAsync(entity);
            return article.MapTo<ArticleDto>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<ArticleDto>> GetAll(GetAllArticleDto input)
        {
            var query = CreateFilteredQuery(input);
            switch (input.Sorting)
            {
                case "Like":
                    query = input.IsAsc ? query.OrderBy(m => m.Like) : query.OrderByDescending(m => m.Like);
                    break;
                case "UnLike":
                    query = input.IsAsc ? query.OrderBy(m => m.UnLike) : query.OrderByDescending(m => m.UnLike);
                    break;
                case "ReadCount":
                    query = input.IsAsc ? query.OrderBy(m => m.ReadCount) : query.OrderByDescending(m => m.ReadCount);
                    break;
                default:
                    query = input.IsAsc ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id);
                    break;
            }
            var totalCount = query.Count();
            var pagedList = await query.PageBy(input).ToListAsync();
            return new PagedResultDto<ArticleDto>(totalCount, pagedList.MapTo<List<ArticleDto>>());
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
                        join article in _articleRepository.GetAll() on user.Id equals article.UserId
                        where user.UserName == input.UserName
                        select article;
            }
            query.WhereIf(!string.IsNullOrEmpty(input.Title), m => m.Title.Contains(input.Title))
                .WhereIf(!string.IsNullOrEmpty(input.ShortDesc), m => m.ShortDesc.Contains(input.ShortDesc));
            return query;
        }

        /// <summary>
        /// 更新Like和Unlike
        /// </summary>
        /// <param name="input"></param>
        public async void UpdataLikeOrUnLikeAsync(UpdateLikeOrUnLikeDto input)
        {
            var entity = await _articleRepository.GetAll().FirstOrDefaultAsync(m => m.Id == input.ArticleId);
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
            await _articleRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新阅读量
        /// </summary>
        /// <param name="articleId"></param>
        public async void UpdateReadCountAsync(long articleId)
        {
            var entity = await _articleRepository.GetAll().FirstOrDefaultAsync(m => m.Id == articleId);
            entity.ReadCount++;
            await _articleRepository.UpdateAsync(entity);
        }
    }
}
