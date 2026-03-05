using System.IO;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Nblity.Abp.FeatureManagement.Blazor.WebAssembly;
using Volo.Abp.Modularity;

namespace Nblity.Abp.TenantManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpTenantManagementBlazorModule),
    typeof(AbpFeatureManagementBlazorWebAssemblyModule),
    typeof(AbpTenantManagementHttpApiClientModule)
    )]
public class AbpTenantManagementBlazorWebAssemblyModule : AbpModule
{

}
