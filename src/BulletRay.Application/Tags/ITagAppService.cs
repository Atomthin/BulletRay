using Abp.Application.Services;
using BulletRay.Tags.Dto;

namespace BulletRay.Tags
{
    public interface ITagAppService : IAsyncCrudAppService<TagDto, int, GetAllTagDto, CreateTagDto, UpdateTagDto>
    {
    }
}
