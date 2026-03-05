using System.Threading.Tasks;

namespace Nblity.Abp.SettingManagement.Blazor;

public interface ISettingComponentContributor
{
    Task ConfigureAsync(SettingComponentCreationContext context);

    Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context);
}
