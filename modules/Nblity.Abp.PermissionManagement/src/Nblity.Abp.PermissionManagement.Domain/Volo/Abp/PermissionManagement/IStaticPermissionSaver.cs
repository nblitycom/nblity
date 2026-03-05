using System.Threading.Tasks;

namespace Nblity.Abp.PermissionManagement;

public interface IStaticPermissionSaver
{
    Task SaveAsync();
}