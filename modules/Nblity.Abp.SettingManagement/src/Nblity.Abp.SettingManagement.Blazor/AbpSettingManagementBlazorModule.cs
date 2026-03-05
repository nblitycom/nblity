using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.Localization;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Nblity.Abp.SettingManagement.Blazor.Menus;
using Nblity.Abp.SettingManagement.Blazor.Settings;
using Nblity.Abp.SettingManagement.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement.Blazor;

[DependsOn(
    typeof(AbpMapperlyModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpSettingManagementApplicationContractsModule)
)]
public class AbpSettingManagementBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpSettingManagementBlazorModule>();

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SettingManagementMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpSettingManagementBlazorModule).Assembly);
        });

        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new EmailingPageContributor());
            options.Contributors.Add(new TimeZonePageContributor());
        });
        
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpSettingManagementResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
