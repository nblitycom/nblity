using System.IO;
using Volo.Abp.VirtualFileSystem;
using Localization.Resources.AbpUi;
using Nblity.Localization;
using Nblity.Abp.Account;
using Nblity.Abp.SettingManagement;
using Nblity.Abp.FeatureManagement;
using Nblity.Abp.Identity;
using Volo.Abp.Modularity;
using Nblity.Abp.PermissionManagement.HttpApi;
using Volo.Abp.Localization;
using Nblity.Abp.TenantManagement;

namespace Nblity;

 [DependsOn(
    typeof(NblityApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule)
    )]
public class NblityHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<NblityResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
