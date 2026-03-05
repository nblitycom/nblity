using System.IO;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Nblity.Abp.PermissionManagement.Blazor.WebAssembly;

namespace Nblity.Abp.Identity.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpIdentityBlazorModule),
    typeof(AbpPermissionManagementBlazorWebAssemblyModule),
    typeof(AbpIdentityHttpApiClientModule)
)]
public class AbpIdentityBlazorWebAssemblyModule : AbpModule
{
}
