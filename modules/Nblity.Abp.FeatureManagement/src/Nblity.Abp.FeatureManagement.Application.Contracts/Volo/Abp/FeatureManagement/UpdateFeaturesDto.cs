using System.Collections.Generic;

namespace Nblity.Abp.FeatureManagement;

public class UpdateFeaturesDto
{
    public List<UpdateFeatureDto> Features { get; set; }
}
