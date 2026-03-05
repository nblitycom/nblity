using System.Collections.Generic;

namespace Nblity.Abp.PermissionManagement;

public class GetResourcePermissionDefinitionListResultDto
{
    public List<ResourcePermissionDefinitionDto> Permissions { get; set; }
}
