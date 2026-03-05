using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Nblity.Abp.Identity;
using Volo.Abp;

namespace Nblity.Abp.Account;

public interface IAccountAppService : IApplicationService
{
    Task<IdentityUserDto> RegisterAsync(RegisterDto input);

    Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input);

    Task<bool> VerifyPasswordResetTokenAsync(VerifyPasswordResetTokenInput input);

    Task ResetPasswordAsync(ResetPasswordDto input);
}
