using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Features;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement;

public interface IFeatureDefinitionSerializer
{
    Task<(FeatureGroupDefinitionRecord[], FeatureDefinitionRecord[])> SerializeAsync(IEnumerable<FeatureGroupDefinition> featureGroups);

    Task<FeatureGroupDefinitionRecord> SerializeAsync(FeatureGroupDefinition featureGroup);

    Task<FeatureDefinitionRecord> SerializeAsync(FeatureDefinition feature, [CanBeNull] FeatureGroupDefinition featureGroup);
}
