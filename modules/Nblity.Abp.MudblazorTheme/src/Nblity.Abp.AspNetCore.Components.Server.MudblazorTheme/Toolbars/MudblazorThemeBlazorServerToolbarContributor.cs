using System.Threading.Tasks;
using Nblity.Abp.AspNetCore.Components.Server.MudblazorTheme.Themes.Mudblazor.Toolbar;
using Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Toolbars;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Components.Server.MudblazorTheme.Toolbars;

public class MudblazorThemeBlazorServerToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == MudblazorToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(UserMenuComponent)));
        }

        if (context.Toolbar.Name == MudblazorToolbars.MainMobile)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(MobileUserMenuComponent)));
        }

        return Task.CompletedTask;
    }
}
