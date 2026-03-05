using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(AbpFeatureManagementDbProperties.ConnectionStringName)]
public interface IFeatureManagementDbContext : IEfCoreDbContext
{
    DbSet<FeatureGroupDefinitionRecord> FeatureGroups { get; }

    DbSet<FeatureDefinitionRecord> Features { get; }

    DbSet<FeatureValue> FeatureValues { get; }
}
