using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Nblity.Abp.Account;
using Volo.Abp.Modularity;
using Nblity.Abp.PermissionManagement;
using Nblity.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;
using Nblity.Abp.FeatureManagement;
using Nblity.Abp.Identity;
using Nblity.Abp.TenantManagement;

namespace Nblity;

[DependsOn(
    typeof(NblityApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpFeatureManagementHttpApiClientModule),
    typeof(AbpAccountHttpApiClientModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
public class NblityHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(NblityApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<NblityHttpApiClientModule>();
        });
    }
}
