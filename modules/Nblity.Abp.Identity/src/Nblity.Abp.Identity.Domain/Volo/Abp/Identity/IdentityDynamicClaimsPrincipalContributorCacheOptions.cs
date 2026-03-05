using System;
using Volo.Abp;

namespace Nblity.Abp.Identity;

public class IdentityDynamicClaimsPrincipalContributorCacheOptions
{
    public TimeSpan CacheAbsoluteExpiration { get; set; }

    public IdentityDynamicClaimsPrincipalContributorCacheOptions()
    {
        CacheAbsoluteExpiration = TimeSpan.FromHours(1);
    }
}
