using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
[DependsOn(typeof(AbpAuthorizationAbstractionsModule))]
public class AbpPermissionManagementApplicationContractsModule : AbpModule
{

}
