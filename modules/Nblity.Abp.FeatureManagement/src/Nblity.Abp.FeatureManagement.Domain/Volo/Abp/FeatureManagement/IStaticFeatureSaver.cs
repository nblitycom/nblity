using System.Threading.Tasks;

namespace Nblity.Abp.FeatureManagement;

public interface IStaticFeatureSaver
{
    Task SaveAsync();
}
