using Abp.Application.Services;
using BulletRay.Comments.Dto;
using BulletRay.CommonDto;

namespace BulletRay.Comments
{
    public interface ICommentAppService : IAsyncCrudAppService<CommentDto, long, GetAllCommentDto, CreateCommentDto>
    {
        void UpdataLikeOrUnLikeAsync(UpdataLikeOrUnLikeDto input);
    }
}
