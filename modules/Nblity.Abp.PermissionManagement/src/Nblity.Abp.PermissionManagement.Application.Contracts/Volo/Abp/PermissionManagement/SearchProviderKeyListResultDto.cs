using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class SearchProviderKeyListResultDto
{
    public List<SearchProviderKeyInfo> Keys { get; set; }
}
