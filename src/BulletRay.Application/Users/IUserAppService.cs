using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BulletRay.Roles.Dto;
using BulletRay.Users.Dto;

namespace BulletRay.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
