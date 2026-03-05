using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpFeatureManagementBlazorModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(AbpFeatureManagementHttpApiClientModule)
)]
public class AbpFeatureManagementBlazorWebAssemblyModule : AbpModule
{
}
