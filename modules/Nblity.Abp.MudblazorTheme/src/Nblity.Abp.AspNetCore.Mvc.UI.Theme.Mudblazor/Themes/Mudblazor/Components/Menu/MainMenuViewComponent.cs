using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Menu;

public class MainMenuViewComponent : AbpViewComponent
{
    protected MenuViewModelProvider MenuViewModelProvider { get; }

    public MainMenuViewComponent(MenuViewModelProvider menuViewModelProvider)
    {
        MenuViewModelProvider = menuViewModelProvider;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await MenuViewModelProvider.GetMenuViewModelAsync();

        return View("~/Themes/Mudblazor/Components/Menu/Default.cshtml", menu);
    }
}