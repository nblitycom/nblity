using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class GetResourceProviderListResultDto
{
    public List<ResourceProviderDto> Providers { get; set; }
}
