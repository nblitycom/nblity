using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement;

public interface IFeatureGroupDefinitionRecordRepository : IBasicRepository<FeatureGroupDefinitionRecord, Guid>
{

}
