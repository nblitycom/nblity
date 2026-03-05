using Volo.Abp.Application.Services;
using Nblity.Abp.FeatureManagement.Localization;

namespace Nblity.Abp.FeatureManagement;

public abstract class FeatureManagementAppServiceBase : ApplicationService
{
    protected FeatureManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(AbpFeatureManagementApplicationModule);
        LocalizationResource = typeof(AbpFeatureManagementResource);
    }
}
