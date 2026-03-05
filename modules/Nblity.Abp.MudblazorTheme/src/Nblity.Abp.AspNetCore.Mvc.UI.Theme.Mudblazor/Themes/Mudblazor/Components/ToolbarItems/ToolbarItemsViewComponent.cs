using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.MainToolbar;

public class ToolbarItemsViewComponent : AbpViewComponent
{
    private IToolbarManager _toolbarManager;

    public ToolbarItemsViewComponent(IToolbarManager toolbarManager)
    {
        _toolbarManager = toolbarManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(string name)
    {
        var toolbar = await _toolbarManager.GetAsync(name ?? MudblazorToolbars.Main);
        return View("~/Themes/Mudblazor/Components/ToolbarItems/Default.cshtml", toolbar);
    }
}
