using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement;

[DependsOn(
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
)]
public class AbpSettingManagementApplicationContractsModule : AbpModule
{
}


