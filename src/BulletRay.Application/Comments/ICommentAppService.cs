using Abp.Application.Services;
using BulletRay.Comments.Dto;

namespace BulletRay.Comments
{
    public interface ICommentAppService : IAsyncCrudAppService<CommentDto, long, GetAllCommentDto, CreateCommentDto>
    {
        void UpdataLikeOrUnLikeAsync(UpdateLikeOrUnLikeDto input);
    }
}
