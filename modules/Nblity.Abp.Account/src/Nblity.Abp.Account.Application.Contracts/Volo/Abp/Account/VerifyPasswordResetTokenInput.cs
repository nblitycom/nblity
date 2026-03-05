using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;

namespace Nblity.Abp.Account;

public class VerifyPasswordResetTokenInput
{
    public Guid UserId { get; set; }

    [Required]
    public string ResetToken { get; set; }
}
