using Microsoft.AspNetCore.Mvc;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Toolbar.MobileLoginLink;

public class MobileLoginLinkViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Mudblazor/Components/Toolbar/MobileLoginLink/Default.cshtml");
    }
}
