using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using BulletRay.Blog;
using BulletRay.Tags.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BulletRay.Tags
{
    public class TagAppService : AsyncCrudAppService<Tag, TagDto, int, GetAllTagDto, CreateTagDto, UpdateTagDto>, ITagAppService
    {
        public TagAppService(IRepository<Tag> tagRepository) : base(tagRepository)
        {
        }

        /// <summary>
        /// 重写Tag GetAll方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<TagDto>> GetAll(GetAllTagDto input)
        {
            var query = Repository.GetAll().WhereIf(!string.IsNullOrEmpty(input.TagKeyStr), m => m.TagName.Contains(input.TagKeyStr)).Skip(input.SkipCount).Take(input.MaxResultCount);
            var pagedList = await query.ToListAsync();
            return new PagedResultDto<TagDto>(query.Count(), pagedList.MapTo<List<TagDto>>());
        }
    }
}
