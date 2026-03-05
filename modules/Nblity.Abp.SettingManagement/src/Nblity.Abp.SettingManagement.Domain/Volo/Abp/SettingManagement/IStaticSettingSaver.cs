using System.Threading.Tasks;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement;

public interface IStaticSettingSaver
{
    Task SaveAsync();
}
