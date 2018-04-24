using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BulletRay.Tags.Dto;

namespace BulletRay.Tags
{
    public interface ITagAppService : IAsyncCrudAppService<TagDto, int, PagedAndSortedResultRequestDto, CreateTagDto, UpdateTagDto>
    {
    }
}
