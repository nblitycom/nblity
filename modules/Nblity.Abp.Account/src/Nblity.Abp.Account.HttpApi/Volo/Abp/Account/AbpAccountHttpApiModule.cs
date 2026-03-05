using System.IO;
using Volo.Abp.VirtualFileSystem;
using Localization.Resources.AbpUi;
using Nblity.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Nblity.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace Nblity.Abp.Account;

[DependsOn(
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpAccountHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AccountResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
