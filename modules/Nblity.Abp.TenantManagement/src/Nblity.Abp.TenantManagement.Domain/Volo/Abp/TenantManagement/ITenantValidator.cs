using System.Threading.Tasks;

namespace Nblity.Abp.TenantManagement;

public interface ITenantValidator
{
    Task ValidateAsync(Tenant tenant);
}
