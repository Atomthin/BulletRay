using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using BulletRay.Blog;
using BulletRay.Comments.Dto;
using BulletRay.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulletRay.Comments
{
    public class CommentAppService : AsyncCrudAppService<Comment, CommentDto, long, GetAllCommentDto, CreateCommentDto>, ICommentAppService
    {
        private readonly IRepository<Comment, long> _commentRepository;
        public CommentAppService(IRepository<Comment, long> commentRepository) : base(commentRepository)
        {
            _commentRepository = commentRepository;
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
            var article = await _commentRepository.InsertAsync(entity);
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
        public async void UpdataLikeOrUnLikeAsync(UpdateLikeOrUnLikeDto input)
        {
            var entity = await _commentRepository.GetAll().FirstOrDefaultAsync(m => m.Id == input.CommentId);
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
            await _commentRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 重写查询过滤条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Comment> CreateFilteredQuery(GetAllCommentDto input)
        {
            var query = base.CreateFilteredQuery(input).Where(m => m.Type == input.Type);
            query = _commentRepository.GetAll().WhereIf(input != null, m => m.ArticleId == input.ArticleId);
            return query;
        }
    }
}
