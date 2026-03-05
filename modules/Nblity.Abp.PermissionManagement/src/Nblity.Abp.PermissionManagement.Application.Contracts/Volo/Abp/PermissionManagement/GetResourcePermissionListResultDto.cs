using System.Collections.Generic;

namespace Nblity.Abp.PermissionManagement;

public class GetResourcePermissionListResultDto
{
    public List<ResourcePermissionGrantInfoDto> Permissions { get; set; }
}
