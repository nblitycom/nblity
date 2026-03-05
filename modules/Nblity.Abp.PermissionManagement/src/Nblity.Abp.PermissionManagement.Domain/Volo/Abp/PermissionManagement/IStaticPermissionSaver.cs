using System.Threading.Tasks;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public interface IStaticPermissionSaver
{
    Task SaveAsync();
}