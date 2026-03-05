using Volo.Abp.Data;

namespace Nblity.Abp.FeatureManagement;

public static class AbpFeatureManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "AbpFeatureManagement";
}
