using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Toolbars;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Nblity.Abp.AspNetCore.Components.WebAssembly.MudblazorTheme.Themes.Mudblazor.Toolbar;

namespace Nblity.Abp.AspNetCore.Components.WebAssembly.MudblazorTheme.Toolbars;

public class MudblazorThemeToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == MudblazorToolbars.Main) 
        {
            //TODO: Can we find a different way to understand if authentication was configured or not?
            var authenticationStateProvider = context.ServiceProvider
                .GetService<AuthenticationStateProvider>();

            if (authenticationStateProvider != null)
            {
                context.Toolbar.Items.Add(new ToolbarItem(typeof(UserMenuComponent)));
            }
        }

        if (context.Toolbar.Name == MudblazorToolbars.MainMobile)
        {
            var authenticationStateProvider = context.ServiceProvider
                .GetService<AuthenticationStateProvider>();

            if (authenticationStateProvider != null)
            {
                context.Toolbar.Items.Add(new ToolbarItem(typeof(MobileUserMenuComponent)));
            }
        }

        return Task.CompletedTask;
    }
}
