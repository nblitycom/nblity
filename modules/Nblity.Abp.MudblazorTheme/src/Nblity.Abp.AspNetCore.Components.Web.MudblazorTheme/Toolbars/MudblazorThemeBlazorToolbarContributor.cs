using System.Threading.Tasks;
using Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Themes.Mudblazor.Toolbar;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Toolbars;

public class MudblazorThemeBlazorToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == MudblazorToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitchComponent)));
        }

        if (context.Toolbar.Name == MudblazorToolbars.MainMobile)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(MobileLanguageSwitchComponent)));
        }

        return Task.CompletedTask;
    }
}
