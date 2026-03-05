﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Toolbar.MobileUserMenu;

public class MobileUserMenuViewComponent : AbpViewComponent
{
    protected IMenuManager MenuManager { get; }

    public MobileUserMenuViewComponent(IMenuManager menuManager)
    {
        MenuManager = menuManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await MenuManager.GetAsync(StandardMenus.User);
        return View("~/Themes/Mudblazor/Components/Toolbar/MobileUserMenu/Default.cshtml", menu);
    }
}
