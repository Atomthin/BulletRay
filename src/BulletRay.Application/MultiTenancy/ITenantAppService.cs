using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BulletRay.MultiTenancy.Dto;

namespace BulletRay.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
