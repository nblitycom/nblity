using System.ComponentModel.DataAnnotations;

namespace Nblity.Abp.Identity;

public class IdentityUserUpdateRolesDto
{
    [Required]
    public string[] RoleNames { get; set; }
}
