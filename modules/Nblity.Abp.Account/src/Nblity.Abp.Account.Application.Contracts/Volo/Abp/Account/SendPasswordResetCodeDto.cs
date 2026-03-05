using System.ComponentModel.DataAnnotations;
using Nblity.Abp.Identity;
using Volo.Abp.Validation;
using Volo.Abp;

namespace Nblity.Abp.Account;

public class SendPasswordResetCodeDto
{
    [Required]
    [EmailAddress]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
    public string Email { get; set; }

    [Required]
    public string AppName { get; set; }

    public string ReturnUrl { get; set; }

    public string ReturnUrlHash { get; set; }
}
