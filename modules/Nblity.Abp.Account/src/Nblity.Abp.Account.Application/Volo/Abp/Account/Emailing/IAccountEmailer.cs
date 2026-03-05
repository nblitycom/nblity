using System.Threading.Tasks;
using Nblity.Abp.Identity;

namespace Nblity.Abp.Account.Emailing;

public interface IAccountEmailer
{
    Task SendPasswordResetLinkAsync(
        IdentityUser user,
        string resetToken,
        string appName,
        string returnUrl = null,
        string returnUrlHash = null
    );
}
