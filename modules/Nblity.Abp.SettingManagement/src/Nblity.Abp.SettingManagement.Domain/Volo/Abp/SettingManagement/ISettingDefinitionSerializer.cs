using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Settings;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement;

public interface ISettingDefinitionSerializer
{
    Task<SettingDefinitionRecord> SerializeAsync(SettingDefinition setting);

    Task<List<SettingDefinitionRecord>> SerializeAsync(IEnumerable<SettingDefinition> settings);
}
