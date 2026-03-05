using System;

namespace Nblity.Abp.PermissionManagement;

public class IsGrantedRequest
{
    public Guid UserId { get; set; }

    public string[] PermissionNames { get; set; }
}
