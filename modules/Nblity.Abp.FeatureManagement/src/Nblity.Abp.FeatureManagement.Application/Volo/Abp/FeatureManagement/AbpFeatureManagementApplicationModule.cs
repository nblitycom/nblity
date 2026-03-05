using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Nblity.Abp.FeatureManagement;

[DependsOn(
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule)
    )]
public class AbpFeatureManagementApplicationModule : AbpModule
{

}
