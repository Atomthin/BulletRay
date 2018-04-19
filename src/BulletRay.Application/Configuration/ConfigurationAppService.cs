using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BulletRay.Configuration.Dto;

namespace BulletRay.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BulletRayAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
