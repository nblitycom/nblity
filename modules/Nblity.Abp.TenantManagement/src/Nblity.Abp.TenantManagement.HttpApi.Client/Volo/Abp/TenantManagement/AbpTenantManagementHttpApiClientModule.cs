using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp;

namespace Nblity.Abp.TenantManagement;

[DependsOn(
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AbpTenantManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(AbpTenantManagementApplicationContractsModule).Assembly,
            TenantManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpTenantManagementHttpApiClientModule>();
        });
    }
}
