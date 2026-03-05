using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Languages;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Toolbar.LanguageSwitch;

public class LanguageSwitchViewComponent : AbpViewComponent
{
    private ThemeLanguageInfoProvider _themeLanguageInfoProvider;

    public LanguageSwitchViewComponent(ThemeLanguageInfoProvider themeLanguageInfoProvider)
    {
        _themeLanguageInfoProvider = themeLanguageInfoProvider;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var languageInfo = await _themeLanguageInfoProvider.GetLanguageSwitchViewComponentModelAsync();
        return View("~/Themes/Mudblazor/Components/Toolbar/LanguageSwitch/Default.cshtml", languageInfo);
    }
}
