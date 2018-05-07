using Abp.Application.Services;
using BulletRay.ArticleCategorys.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BulletRay.ArticleCategorys
{
    public interface IArticleCategoryAppService : IAsyncCrudAppService<ArticleCategoryDto, int, GetAllArticleCategoryDto, CreateArticleCategoryDto, UpdateArticleCategoryDto>
    {
        Task<List<ArticleCategoryIdDto>> GetArticleCategoryIdList();
    }
}
