using Volo.Abp;
﻿using System.Collections.Generic;

namespace Nblity.Abp.PermissionManagement;

public class GetPermissionListResultDto
{
    public string EntityDisplayName { get; set; }

    public List<PermissionGroupDto> Groups { get; set; }
}
