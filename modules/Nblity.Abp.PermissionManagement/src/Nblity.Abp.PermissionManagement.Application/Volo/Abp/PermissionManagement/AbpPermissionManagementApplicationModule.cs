using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

[DependsOn(
    typeof(AbpPermissionManagementDomainModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule)
    )]
public class AbpPermissionManagementApplicationModule : AbpModule
{

}
