using System.Threading.Tasks;
using Abp.Application.Services;
using BulletRay.Authorization.Accounts.Dto;

namespace BulletRay.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
