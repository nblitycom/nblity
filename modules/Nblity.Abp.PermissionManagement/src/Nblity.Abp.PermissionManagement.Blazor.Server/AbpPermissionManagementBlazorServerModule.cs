using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement.Blazor.Server;

[DependsOn(
    typeof(AbpPermissionManagementBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
)]
public class AbpPermissionManagementBlazorServerModule : AbpModule
{
}
