using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Languages;

public interface ILanguagePlatformManager
{
    Task ChangeAsync(LanguageInfo newLanguage);

    Task<LanguageInfo> GetCurrentAsync();
}
