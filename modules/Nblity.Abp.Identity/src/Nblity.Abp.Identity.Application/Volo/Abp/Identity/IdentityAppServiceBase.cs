using Volo.Abp.Application.Services;
using Nblity.Abp.Identity.Localization;
using Volo.Abp;

namespace Nblity.Abp.Identity;

public abstract class IdentityAppServiceBase : ApplicationService
{
    protected IdentityAppServiceBase()
    {
        ObjectMapperContext = typeof(AbpIdentityApplicationModule);
        LocalizationResource = typeof(IdentityResource);
    }
}
