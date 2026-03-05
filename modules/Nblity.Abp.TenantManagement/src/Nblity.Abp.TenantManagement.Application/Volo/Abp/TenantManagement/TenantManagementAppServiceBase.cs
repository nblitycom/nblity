using Volo.Abp.Application.Services;
using Nblity.Abp.TenantManagement.Localization;

namespace Nblity.Abp.TenantManagement;

public abstract class TenantManagementAppServiceBase : ApplicationService
{
    protected TenantManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(AbpTenantManagementApplicationModule);
        LocalizationResource = typeof(AbpTenantManagementResource);
    }
}
