using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;

namespace Nblity.Abp.Identity;

public interface IIdentityDataSeeder
{
    Task<IdentityDataSeedResult> SeedAsync(
        [NotNull] string adminEmail,
        [NotNull] string adminPassword,
        Guid? tenantId = null,
        string? adminUserName = null);
}
