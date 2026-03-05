using Volo.Abp;
﻿using System.Collections.Generic;

namespace Nblity.Abp.FeatureManagement;

public class FeatureGroupDto
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public List<FeatureDto> Features { get; set; }

    public string GetNormalizedGroupName()
    {
        return Name.Replace(".", "_");
    }
}
