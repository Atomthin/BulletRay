using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using BulletRay.Blog;
using BulletRay.Comments.Dto;
using BulletRay.CommonDto;
using BulletRay.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulletRay.Comments
{
    public class CommentAppService : AsyncCrudAppService<Comment, CommentDto, long, GetAllCommentDto, CreateCommentDto>, ICommentAppService
    {
        public CommentAppService(IRepository<Comment, long> commentRepository) : base(commentRepository)
        {
        }

        /// <summary>
        /// 重写创建Comment
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<CommentDto> Create(CreateCommentDto input)
        {
            var entity = ObjectMapper.Map<Comment>(input);
            entity.UserId = AbpSession.UserId.Value;
            var article = await Repository.InsertAsync(entity);
            return MapToEntityDto(article);
        }

        /// <summary>
        /// 重写分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<CommentDto>> GetAll(GetAllCommentDto input)
        {
            var query = CreateFilteredQuery(input);
            var totalCount = query.Count();
            var pagedList = await query.PageBy(input).ToListAsync();
            return new PagedResultDto<CommentDto>(totalCount, pagedList.MapTo<List<CommentDto>>());
        }

        /// <summary>
        /// 更新Like和Unlike
        /// </summary>
        /// <param name="input"></param>
        public async void UpdataLikeOrUnLikeAsync(UpdataLikeOrUnLikeDto input)
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
        /// 重写查询过滤条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Comment> CreateFilteredQuery(GetAllCommentDto input)
        {
            var query = base.CreateFilteredQuery(input).Where(m => m.Type == input.Type);
            query = Repository.GetAll().WhereIf(input.ArticleId != null, m => m.ArticleId == input.ArticleId);
            return query;
        }
    }
}
