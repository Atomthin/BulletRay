using Abp.Application.Services;
using BulletRay.Articles.Dto;
using System.Threading.Tasks;

namespace BulletRay.Articles
{
    public interface IArticleAppService : IAsyncCrudAppService<ArticleDto, long, GetAllArticleDto, CreateArticleDto, UpdateArticleDto>
    {
        void UpdataLikeOrUnLikeAsync(UpdateLikeOrUnLikeDto input);
        void UpdateReadCountAsync(long articleId);
    }
}
