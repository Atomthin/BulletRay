using Abp.Application.Services;
using BulletRay.Articles.Dto;
using BulletRay.CommonDto;

namespace BulletRay.Articles
{
    public interface IArticleAppService : IAsyncCrudAppService<ArticleDto, long, GetAllArticleDto, CreateArticleDto, UpdateArticleDto>
    {
        void UpdateLikeOrUnLikeAsync(UpdataLikeOrUnLikeDto input);
        void UpdateReadCountAsync(long articleId);
    }
}
