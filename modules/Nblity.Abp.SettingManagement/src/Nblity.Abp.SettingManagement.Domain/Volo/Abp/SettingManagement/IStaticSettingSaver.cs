using System.Threading.Tasks;

namespace Nblity.Abp.SettingManagement;

public interface IStaticSettingSaver
{
    Task SaveAsync();
}
