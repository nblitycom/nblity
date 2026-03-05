using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor;

[ThemeName(Name)]
public class MudblazorTheme : ITheme, ITransientDependency
{
    public const string Name = "Mudblazor";

    public virtual string GetLayout(string name, bool fallbackToDefault = true)
    {
        switch (name)
        {
            case StandardLayouts.Application:
                return "~/Themes/Mudblazor/Layouts/Application.cshtml";
            case StandardLayouts.Account:
                return "~/Themes/Mudblazor/Layouts/Account.cshtml";
            case StandardLayouts.Empty:
                return "~/Themes/Mudblazor/Layouts/Empty.cshtml";
            default:
                return fallbackToDefault ? "~/Themes/Mudblazor/Layouts/Application.cshtml" : null;
        }
    }
}
