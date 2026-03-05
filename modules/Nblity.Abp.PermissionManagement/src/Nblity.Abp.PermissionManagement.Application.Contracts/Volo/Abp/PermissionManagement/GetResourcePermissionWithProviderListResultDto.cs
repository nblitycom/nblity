using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class GetResourcePermissionWithProviderListResultDto
{
    public List<ResourcePermissionWithProdiverGrantInfoDto> Permissions { get; set; }
}
