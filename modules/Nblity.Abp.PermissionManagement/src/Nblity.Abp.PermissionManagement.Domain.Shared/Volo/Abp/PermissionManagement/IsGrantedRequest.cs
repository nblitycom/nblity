using System;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class IsGrantedRequest
{
    public Guid UserId { get; set; }

    public string[] PermissionNames { get; set; }
}
