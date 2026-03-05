using System.IO;
using Volo.Abp.VirtualFileSystem;
using Nblity.Abp.PermissionManagement;
using Nblity.Abp.SettingManagement;
using Nblity.Abp.Account;
using Nblity.Abp.Identity;
using Volo.Abp.Mapperly;
using Nblity.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Nblity.Abp.TenantManagement;

namespace Nblity;

[DependsOn(
    typeof(NblityDomainModule),
    typeof(NblityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class NblityApplicationModule : AbpModule
{

}
