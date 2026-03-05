using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpSettingManagementBlazorModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
public class AbpSettingManagementBlazorWebAssemblyModule : AbpModule
{
}
