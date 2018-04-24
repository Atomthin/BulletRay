using Abp.Application.Services;
using BulletRay.ArticleCategorys.Dto;

namespace BulletRay.ArticleCategorys
{
    public interface IArticleCategoryAppService : IAsyncCrudAppService<ArticleCategoryDto, int, GetAllArticleCategoryDto, CreateArticleCategoryDto, UpdateArticleCategoryDto>
    {
    }
}
