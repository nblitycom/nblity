using System.Collections.Generic;
using Volo.Abp.Localization;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Languages;
public class ThemeLanguageInfo
{
    public bool HasLanguages => Languages != null && Languages.Count > 1;

    public LanguageInfo CurrentLanguage { get; set; }

    public IReadOnlyList<LanguageInfo> Languages { get; set; }
}
