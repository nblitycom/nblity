using System;
using Volo.Abp.Domain.Repositories;

namespace Nblity.Abp.FeatureManagement;

public interface IFeatureGroupDefinitionRecordRepository : IBasicRepository<FeatureGroupDefinitionRecord, Guid>
{

}
