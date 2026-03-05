using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement;

public interface IEmailSettingsAppService : IApplicationService
{
    Task<EmailSettingsDto> GetAsync();

    Task UpdateAsync(UpdateEmailSettingsDto input);

    Task SendTestEmailAsync(SendTestEmailInput input);
}
