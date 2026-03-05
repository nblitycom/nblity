using System.Collections.Generic;

namespace Nblity.Abp.FeatureManagement;

public class GetFeatureListResultDto
{
    public List<FeatureGroupDto> Groups { get; set; }
}
