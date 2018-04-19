using System.Threading.Tasks;
using BulletRay.Configuration.Dto;

namespace BulletRay.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
