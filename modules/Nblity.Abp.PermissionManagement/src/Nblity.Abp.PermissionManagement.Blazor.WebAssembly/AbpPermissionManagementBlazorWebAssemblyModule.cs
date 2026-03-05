using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpPermissionManagementBlazorModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(AbpPermissionManagementHttpApiClientModule)
)]
public class AbpPermissionManagementBlazorWebAssemblyModule : AbpModule
{
}
