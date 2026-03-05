﻿using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Brand;

public class MainNavbarBrandViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Mudblazor/Components/Brand/Default.cshtml");
    }
}
