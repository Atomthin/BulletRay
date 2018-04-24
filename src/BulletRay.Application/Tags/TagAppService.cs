using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using BulletRay.Blog;
using BulletRay.Tags.Dto;

namespace BulletRay.Tags
{
    public class TagAppService : AsyncCrudAppService<Tag, TagDto, int, PagedAndSortedResultRequestDto, CreateTagDto, UpdateTagDto>
    {
        public TagAppService(IRepository<Tag> tagRepository) : base(tagRepository)
        {
        }
    }
}
