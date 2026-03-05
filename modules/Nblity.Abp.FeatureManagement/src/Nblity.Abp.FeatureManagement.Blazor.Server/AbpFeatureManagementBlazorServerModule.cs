using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement.Blazor.Server;

[DependsOn(
    typeof(AbpFeatureManagementBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class AbpFeatureManagementBlazorServerModule : AbpModule
{

}
