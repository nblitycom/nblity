using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.Features;
using Nblity.Abp.SettingManagement.Blazor.Pages.SettingManagement.TimeZoneSettingGroup;
using Nblity.Abp.SettingManagement.Localization;
using Volo.Abp.Timing;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement.Blazor.Settings;

public class TimeZonePageContributor : ISettingComponentContributor
{
    public async Task ConfigureAsync(SettingComponentCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AbpSettingManagementResource>>();
        if (await CheckPermissionsAsync(context))
        {
            context.Groups.Add(
                new SettingComponentGroup(
                    "Volo.Abp.TimeZone",
                    l["Menu:TimeZone"],
                    typeof(TimeZoneSettingGroupViewComponent)
                )
            );
        }
    }

    public async Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return context.ServiceProvider.GetRequiredService<IClock>().SupportsMultipleTimezone
            && await authorizationService.IsGrantedAsync(SettingManagementPermissions.TimeZone);
    }
}
