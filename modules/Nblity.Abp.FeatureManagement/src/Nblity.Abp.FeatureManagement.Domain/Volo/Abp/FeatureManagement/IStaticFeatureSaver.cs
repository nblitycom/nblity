using System.Threading.Tasks;
using Volo.Abp;

namespace Nblity.Abp.FeatureManagement;

public interface IStaticFeatureSaver
{
    Task SaveAsync();
}
