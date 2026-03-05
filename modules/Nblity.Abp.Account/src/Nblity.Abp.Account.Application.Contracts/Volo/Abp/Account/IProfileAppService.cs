using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Nblity.Abp.Identity;

namespace Nblity.Abp.Account;

public interface IProfileAppService : IApplicationService
{
    Task<ProfileDto> GetAsync();

    Task<ProfileDto> UpdateAsync(UpdateProfileDto input);

    Task ChangePasswordAsync(ChangePasswordInput input);
}
