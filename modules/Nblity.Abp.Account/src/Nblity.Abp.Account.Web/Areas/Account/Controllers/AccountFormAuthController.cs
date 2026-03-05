using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nblity.Abp.Account.Localization;
using Nblity.Abp.Account.Settings;
using Nblity.Abp.Identity;
using Nblity.Abp.Identity.AspNetCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IdentityUser = Nblity.Abp.Identity.IdentityUser;

namespace Nblity.Abp.Account.Web.Areas.Account.Controllers;

[AllowAnonymous]
[Route("auth")]
[ApiExplorerSettings(IgnoreApi = true)]
[IgnoreAntiforgeryToken]
public class AccountFormAuthController : AbpControllerBase
{
    protected SignInManager<IdentityUser> SignInManager { get; }
    protected IdentityUserManager UserManager { get; }
    protected ISettingProvider SettingProvider { get; }
    protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }
    protected IOptions<IdentityOptions> IdentityOptions { get; }
    protected IdentityDynamicClaimsPrincipalContributorCache IdentityDynamicClaimsPrincipalContributorCache { get; }
    protected IAccountAppService AccountAppService { get; }

    public AccountFormAuthController(
        SignInManager<IdentityUser> signInManager,
        IdentityUserManager userManager,
        ISettingProvider settingProvider,
        IdentitySecurityLogManager identitySecurityLogManager,
        IOptions<IdentityOptions> identityOptions,
        IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache,
        IAccountAppService accountAppService)
    {
        LocalizationResource = typeof(AccountResource);

        SignInManager = signInManager;
        UserManager = userManager;
        SettingProvider = settingProvider;
        IdentitySecurityLogManager = identitySecurityLogManager;
        IdentityOptions = identityOptions;
        IdentityDynamicClaimsPrincipalContributorCache = identityDynamicClaimsPrincipalContributorCache;
        AccountAppService = accountAppService;
    }

    [HttpPost("login")]
    public virtual async Task<IActionResult> Login(
        [FromForm] string userNameOrEmailAddress,
        [FromForm] string password,
        [FromForm] bool rememberMe,
        [FromForm] string returnUrl,
        [FromForm] string returnUrlHash)
    {
        try
        {
            if (!await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
            {
                return BuildErrorRedirect("/Account/Login", "LocalLoginDisabled", returnUrl, returnUrlHash);
            }

            if (string.IsNullOrWhiteSpace(userNameOrEmailAddress) || string.IsNullOrWhiteSpace(password))
            {
                return BuildErrorRedirect("/Account/Login", "InvalidInput", returnUrl, returnUrlHash);
            }

            await ReplaceEmailToUsernameIfNeeded(ref userNameOrEmailAddress);

            await IdentityOptions.SetAsync();

            var result = await SignInManager.PasswordSignInAsync(
                userNameOrEmailAddress,
                password,
                rememberMe,
                true
            );

            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = result.ToIdentitySecurityLogAction(),
                UserName = userNameOrEmailAddress
            });

            if (result.RequiresTwoFactor)
            {
                return BuildErrorRedirect("/Account/Login", "RequiresTwoFactor", returnUrl, returnUrlHash);
            }

            if (result.IsLockedOut)
            {
                return BuildErrorRedirect("/Account/Login", "UserLockedOut", returnUrl, returnUrlHash);
            }

            if (result.IsNotAllowed)
            {
                return BuildErrorRedirect("/Account/Login", "LoginIsNotAllowed", returnUrl, returnUrlHash);
            }

            if (!result.Succeeded)
            {
                return BuildErrorRedirect("/Account/Login", "InvalidUserNameOrPassword", returnUrl, returnUrlHash);
            }

            var user = await UserManager.FindByNameAsync(userNameOrEmailAddress) ??
                       await UserManager.FindByEmailAsync(userNameOrEmailAddress);

            Debug.Assert(user != null, nameof(user) + " != null");

            await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);

            return RedirectSafely(returnUrl, returnUrlHash);
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "Login failed for user: {UserName}", userNameOrEmailAddress);
            return BuildErrorRedirect("/Account/Login", "InvalidUserNameOrPassword", returnUrl, returnUrlHash);
        }
    }

    [HttpGet("logout")]
    public virtual async Task<IActionResult> Logout(string returnUrl = null, string returnUrlHash = null)
    {
        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
        {
            Identity = IdentitySecurityLogIdentityConsts.Identity,
            Action = IdentitySecurityLogActionConsts.Logout
        });

        await SignInManager.SignOutAsync();

        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            return RedirectSafely(returnUrl, returnUrlHash);
        }

        if (await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
        {
            return Redirect("/Account/Login");
        }

        return Redirect("/");
    }

    [HttpPost("register")]
    public virtual async Task<IActionResult> Register(
        [FromForm] string userName,
        [FromForm] string emailAddress,
        [FromForm] string password,
        [FromForm] string returnUrl,
        [FromForm] string returnUrlHash)
    {
        try
        {
            var enableLocalRegister = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin) &&
                                     await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled);

            if (!enableLocalRegister)
            {
                return BuildErrorRedirect("/Account/Register", "SelfRegistrationDisabled", returnUrl, returnUrlHash);
            }

            var userDto = await AccountAppService.RegisterAsync(
                new RegisterDto
                {
                    AppName = "MVC",
                    EmailAddress = emailAddress,
                    Password = password,
                    UserName = userName
                }
            );

            var user = await UserManager.GetByIdAsync(userDto.Id);
            await SignInManager.SignInAsync(user, isPersistent: true);

            await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);

            return RedirectSafely(returnUrl, returnUrlHash);
        }
        catch (BusinessException ex)
        {
            Logger.LogWarning(ex, "Registration failed for user: {UserName}", userName);
            return BuildErrorRedirect("/Account/Register", Uri.EscapeDataString(ex.Message ?? "RegistrationFailed"), returnUrl, returnUrlHash);
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "Registration failed for user: {UserName}", userName);
            return BuildErrorRedirect("/Account/Register", "RegistrationFailed", returnUrl, returnUrlHash);
        }
    }

    [HttpPost("forgot-password")]
    public virtual async Task<IActionResult> ForgotPassword(
        [FromForm] string email,
        [FromForm] string returnUrl,
        [FromForm] string returnUrlHash)
    {
        try
        {
            await AccountAppService.SendPasswordResetCodeAsync(
                new SendPasswordResetCodeDto
                {
                    Email = email,
                    AppName = "MVC",
                    ReturnUrl = returnUrl,
                    ReturnUrlHash = returnUrlHash
                }
            );

            var redirectUrl = $"/Account/PasswordResetLinkSent?returnUrl={Uri.EscapeDataString(returnUrl ?? "")}&returnUrlHash={Uri.EscapeDataString(returnUrlHash ?? "")}";
            return Redirect(redirectUrl);
        }
        catch (UserFriendlyException ex)
        {
            Logger.LogWarning(ex, "Forgot password failed for email: {Email}", email);
            return BuildErrorRedirect("/Account/ForgotPassword", Uri.EscapeDataString(ex.Message ?? "ForgotPasswordFailed"), returnUrl, returnUrlHash);
        }
    }

    [HttpPost("reset-password")]
    public virtual async Task<IActionResult> ResetPassword(
        [FromForm] Guid userId,
        [FromForm] string resetToken,
        [FromForm] string password,
        [FromForm] string confirmPassword,
        [FromForm] string returnUrl,
        [FromForm] string returnUrlHash)
    {
        try
        {
            if (password != confirmPassword)
            {
                return BuildErrorRedirect($"/Account/ResetPassword?userId={userId}&resetToken={Uri.EscapeDataString(resetToken ?? "")}", "PasswordsDoNotMatch", returnUrl, returnUrlHash);
            }

            await AccountAppService.ResetPasswordAsync(
                new ResetPasswordDto
                {
                    UserId = userId,
                    ResetToken = resetToken,
                    Password = password
                }
            );

            var redirectUrl = $"/Account/ResetPasswordConfirmation?returnUrl={Uri.EscapeDataString(returnUrl ?? "")}&returnUrlHash={Uri.EscapeDataString(returnUrlHash ?? "")}";
            return Redirect(redirectUrl);
        }
        catch (AbpIdentityResultException ex)
        {
            Logger.LogWarning(ex, "Reset password failed for user: {UserId}", userId);
            return BuildErrorRedirect($"/Account/ResetPassword?userId={userId}&resetToken={Uri.EscapeDataString(resetToken ?? "")}", Uri.EscapeDataString(ex.Message ?? "ResetPasswordFailed"), returnUrl, returnUrlHash);
        }
        catch (AbpValidationException ex)
        {
            Logger.LogWarning(ex, "Reset password validation failed for user: {UserId}", userId);
            return BuildErrorRedirect($"/Account/ResetPassword?userId={userId}&resetToken={Uri.EscapeDataString(resetToken ?? "")}", "ValidationFailed", returnUrl, returnUrlHash);
        }
    }

    [HttpPost("external-login")]
    public virtual Task<IActionResult> ExternalLogin(
        [FromForm] string provider,
        [FromForm] string returnUrl,
        [FromForm] string returnUrlHash)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "AccountFormAuth", new { returnUrl, returnUrlHash });
        var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        properties.Items["scheme"] = provider;

        return Task.FromResult<IActionResult>(Challenge(properties, provider));
    }

    [HttpGet("external-login-callback")]
    public virtual async Task<IActionResult> ExternalLoginCallback(string returnUrl = "", string returnUrlHash = "", string remoteError = null)
    {
        if (remoteError != null)
        {
            Logger.LogWarning("External login callback error: {RemoteError}", remoteError);
            return Redirect("/Account/Login");
        }

        await IdentityOptions.SetAsync();

        var loginInfo = await SignInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            Logger.LogWarning("External login info is not available");
            return Redirect("/Account/Login");
        }

        var result = await SignInManager.ExternalLoginSignInAsync(
            loginInfo.LoginProvider,
            loginInfo.ProviderKey,
            isPersistent: false,
            bypassTwoFactor: true
        );

        if (!result.Succeeded)
        {
            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
            {
                Identity = IdentitySecurityLogIdentityConsts.IdentityExternal,
                Action = "Login" + result
            });
        }

        if (result.IsLockedOut)
        {
            return BuildErrorRedirect("/Account/Login", "UserLockedOut", returnUrl, returnUrlHash);
        }

        if (result.IsNotAllowed)
        {
            return BuildErrorRedirect("/Account/Login", "LoginIsNotAllowed", returnUrl, returnUrlHash);
        }

        if (result.Succeeded)
        {
            var user = await UserManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);
            if (user != null)
            {
                await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);
            }

            return RedirectSafely(returnUrl, returnUrlHash);
        }

        var email = loginInfo.Principal.FindFirstValue(Volo.Abp.Security.Claims.AbpClaimTypes.Email) ??
                    loginInfo.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email);

        if (string.IsNullOrWhiteSpace(email))
        {
            return Redirect($"/Account/Register?IsExternalLogin=true&ExternalLoginAuthSchema={loginInfo.LoginProvider}&returnUrl={Uri.EscapeDataString(returnUrl ?? "")}");
        }

        var existingUser = await UserManager.FindByEmailAsync(email);
        if (existingUser == null)
        {
            return Redirect($"/Account/Register?IsExternalLogin=true&ExternalLoginAuthSchema={loginInfo.LoginProvider}&returnUrl={Uri.EscapeDataString(returnUrl ?? "")}");
        }

        if (await UserManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey) == null)
        {
            (await UserManager.AddLoginAsync(existingUser, loginInfo)).CheckErrors();
        }

        await SignInManager.SignInAsync(existingUser, false);

        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
        {
            Identity = IdentitySecurityLogIdentityConsts.IdentityExternal,
            Action = result.ToIdentitySecurityLogAction(),
            UserName = existingUser.Name
        });

        await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(existingUser.Id, existingUser.TenantId);

        return RedirectSafely(returnUrl, returnUrlHash);
    }

    private IActionResult RedirectSafely(string returnUrl, string returnUrlHash)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                return Redirect(returnUrl + returnUrlHash);
            }
            return Redirect(returnUrl);
        }

        return Redirect("/");
    }

    private IActionResult BuildErrorRedirect(string basePath, string error, string returnUrl, string returnUrlHash)
    {
        var url = $"{basePath}?error={Uri.EscapeDataString(error ?? "")}";
        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            url += $"&returnUrl={Uri.EscapeDataString(returnUrl)}";
        }
        if (!string.IsNullOrWhiteSpace(returnUrlHash))
        {
            url += $"&returnUrlHash={Uri.EscapeDataString(returnUrlHash)}";
        }
        return Redirect(url);
    }

    private async Task ReplaceEmailToUsernameIfNeeded(ref string userNameOrEmailAddress)
    {
        if (!ValidationHelper.IsValidEmailAddress(userNameOrEmailAddress))
        {
            return;
        }

        var userByUsername = await UserManager.FindByNameAsync(userNameOrEmailAddress);
        if (userByUsername != null)
        {
            return;
        }

        var userByEmail = await UserManager.FindByEmailAsync(userNameOrEmailAddress);
        if (userByEmail == null)
        {
            return;
        }

        userNameOrEmailAddress = userByEmail.UserName;
    }
}
