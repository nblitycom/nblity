using System;
using Volo.Abp;

namespace Nblity.Abp.Identity;

public class RoleFinderResult
{
    public Guid Id { get; set; }

    public string RoleName { get; set; }
}
