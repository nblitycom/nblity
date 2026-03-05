using System.IO;
using Volo.Abp.VirtualFileSystem;
using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Nblity.Abp.FeatureManagement;
using Nblity.Abp.FeatureManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Nblity.Abp.TenantManagement.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Nblity.Abp.TenantManagement;

[DependsOn(
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpAspNetCoreMvcModule)
    )]
public class AbpTenantManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpTenantManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpTenantManagementResource>()
                .AddBaseTypes(
                    typeof(AbpFeatureManagementResource),
                    typeof(AbpUiResource));
        });
    }
}
