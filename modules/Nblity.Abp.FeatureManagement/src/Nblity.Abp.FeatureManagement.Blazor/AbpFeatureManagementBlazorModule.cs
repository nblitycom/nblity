using System.IO;
using Volo.Abp.VirtualFileSystem;
using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Nblity.Abp.FeatureManagement.Blazor.Settings;
using Nblity.Abp.FeatureManagement.Localization;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Nblity.Abp.SettingManagement.Blazor;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement.Blazor;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpFeaturesModule),
    typeof(AbpSettingManagementBlazorModule)
)]
public class AbpFeatureManagementBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new FeatureSettingManagementComponentContributor());
        });
        
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpFeatureManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
