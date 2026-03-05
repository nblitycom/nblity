using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement;

public interface IFeatureDefinitionRecordRepository : IBasicRepository<FeatureDefinitionRecord, Guid>
{
    Task<FeatureDefinitionRecord> FindByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}
