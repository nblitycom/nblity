using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp;

namespace Nblity.Abp.Account;

public class ResetPasswordDto
{
    public Guid UserId { get; set; }

    [Required]
    public string ResetToken { get; set; }

    [Required]
    [DisableAuditing]
    public string Password { get; set; }
}
