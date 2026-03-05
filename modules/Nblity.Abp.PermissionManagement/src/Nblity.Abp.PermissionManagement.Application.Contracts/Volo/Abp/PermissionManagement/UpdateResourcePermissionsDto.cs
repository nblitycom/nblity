using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class UpdateResourcePermissionsDto
{
    public string ProviderName { get; set; }

    public string ProviderKey { get; set; }

    public List<string> Permissions { get; set; }
}
