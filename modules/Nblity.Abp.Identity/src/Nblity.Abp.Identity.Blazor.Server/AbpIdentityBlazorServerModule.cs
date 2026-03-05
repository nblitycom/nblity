using System.IO;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;
using Nblity.Abp.PermissionManagement.Blazor.Server;
using Volo.Abp;

namespace Nblity.Abp.Identity.Blazor.Server;

[DependsOn(
    typeof(AbpIdentityBlazorModule),
    typeof(AbpPermissionManagementBlazorServerModule)
)]
public class AbpIdentityBlazorServerModule : AbpModule
{
}
