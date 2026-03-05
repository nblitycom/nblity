using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Languages;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Toolbar.MobileLanguageSwitch;

public class MobileLanguageSwitchViewComponent : AbpViewComponent
{
    private ThemeLanguageInfoProvider _themeLanguageInfoProvider;

    public MobileLanguageSwitchViewComponent(ThemeLanguageInfoProvider themeLanguageInfoProvider)
    {
        _themeLanguageInfoProvider = themeLanguageInfoProvider;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var languageInfo = await _themeLanguageInfoProvider.GetLanguageSwitchViewComponentModelAsync();
        return View("~/Themes/Mudblazor/Components/Toolbar/MobileLanguageSwitch/Default.cshtml", languageInfo);
    }
}
