using System.IO;
using Volo.Abp.VirtualFileSystem;
using Nblity.Abp.Account;
using Volo.Abp.Modularity;
using Nblity.Abp.PermissionManagement;
using Nblity.Abp.SettingManagement;
using Nblity.Abp.FeatureManagement;
using Nblity.Abp.Identity;
using Nblity.Abp.TenantManagement;

namespace Nblity;

[DependsOn(
    typeof(NblityDomainSharedModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule)
)]
public class NblityApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        NblityDtoExtensions.Configure();
    }
}
