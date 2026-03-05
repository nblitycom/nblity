using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Nblity.Abp.PermissionManagement.Localization;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement.Blazor;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpPermissionManagementApplicationContractsModule)
    )]
public class AbpPermissionManagementBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpPermissionManagementResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
