using System;
using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.Identity;

public class IdentityUserIdWithRoleNames
{
    public Guid Id { get; set; }

    public string[] RoleNames { get; set; }
}