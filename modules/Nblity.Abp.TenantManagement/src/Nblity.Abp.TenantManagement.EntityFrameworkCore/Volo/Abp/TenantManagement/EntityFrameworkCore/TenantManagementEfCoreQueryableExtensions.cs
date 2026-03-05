using System.Linq;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Nblity.Abp.TenantManagement.EntityFrameworkCore;

public static class TenantManagementEfCoreQueryableExtensions
{
    public static IQueryable<Tenant> IncludeDetails(this IQueryable<Tenant> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.ConnectionStrings);
    }
}
