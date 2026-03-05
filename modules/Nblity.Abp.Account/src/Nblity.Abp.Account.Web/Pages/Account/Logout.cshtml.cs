using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nblity.Abp.Account.Settings;
using Nblity.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp;

namespace Nblity.Abp.Account.Web.Pages.Account;

public class LogoutModel : AccountPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public string ReturnUrlHash { get; set; }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
        {
            Identity = IdentitySecurityLogIdentityConsts.Identity,
            Action = IdentitySecurityLogActionConsts.Logout
        });

        await SignInManager.SignOutAsync();
        if (ReturnUrl != null)
        {
            return await RedirectSafelyAsync(ReturnUrl, ReturnUrlHash);
        }

        if (await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
        {
            return RedirectToPage("/Account/Login");
        }

        return RedirectToPage("/");
    }

    public virtual Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }
}
