using Volo.Abp.Application.Services;
using Nblity.Abp.SettingManagement.Localization;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement;

public abstract class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(AbpSettingManagementApplicationModule);
        LocalizationResource = typeof(AbpSettingManagementResource);
    }
}
