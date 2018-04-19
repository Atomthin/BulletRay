using System.Threading.Tasks;
using Abp.Application.Services;
using BulletRay.Sessions.Dto;

namespace BulletRay.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
