using System.Collections.Generic;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public class GetResourcePermissionDefinitionListResultDto
{
    public List<ResourcePermissionDefinitionDto> Permissions { get; set; }
}
