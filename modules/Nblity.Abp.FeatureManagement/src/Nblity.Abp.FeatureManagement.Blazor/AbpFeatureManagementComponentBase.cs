using Volo.Abp.AspNetCore.Components;
using Nblity.Abp.FeatureManagement.Localization;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement.Blazor;

public abstract class AbpFeatureManagementComponentBase : AbpComponentBase
{
    protected AbpFeatureManagementComponentBase()
    {
        LocalizationResource = typeof(AbpFeatureManagementResource);
    }
}
