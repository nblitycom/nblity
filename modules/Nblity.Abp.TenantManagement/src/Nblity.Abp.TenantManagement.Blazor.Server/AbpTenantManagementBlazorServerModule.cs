using System.IO;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Nblity.Abp.FeatureManagement.Blazor.Server;
using Volo.Abp.Modularity;

namespace Nblity.Abp.TenantManagement.Blazor.Server;

[DependsOn(
    typeof(AbpTenantManagementBlazorModule),
    typeof(AbpFeatureManagementBlazorServerModule)
    )]
public class AbpTenantManagementBlazorServerModule : AbpModule
{

}
