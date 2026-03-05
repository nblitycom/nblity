using System.Collections.Generic;

namespace Nblity.Abp.PermissionManagement;

public class GetResourcePermissionWithProviderListResultDto
{
    public List<ResourcePermissionWithProdiverGrantInfoDto> Permissions { get; set; }
}
