using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class GetResourcePermissionListResultDto
{
    public List<ResourcePermissionGrantInfoDto> Permissions { get; set; }
}
