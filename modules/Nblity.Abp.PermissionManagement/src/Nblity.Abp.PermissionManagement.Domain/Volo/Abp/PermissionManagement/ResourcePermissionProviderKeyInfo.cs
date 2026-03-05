using Volo.Abp.ObjectExtending;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class ResourcePermissionProviderKeyInfo
{
    public string ProviderKey { get; set; }

    public string ProviderDisplayName { get; set; }

    public ResourcePermissionProviderKeyInfo(string providerKey, string providerDisplayName)
    {
        ProviderKey = providerKey;
        ProviderDisplayName = providerDisplayName;
    }
}
