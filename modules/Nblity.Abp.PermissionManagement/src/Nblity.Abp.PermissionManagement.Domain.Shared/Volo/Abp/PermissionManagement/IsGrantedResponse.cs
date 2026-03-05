using System;
using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class IsGrantedResponse
{
    public Guid UserId { get; set; }

    public Dictionary<string, bool> Permissions { get; set; }
}
