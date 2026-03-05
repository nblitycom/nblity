﻿using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.ContentTitle;

public class ContentTitleViewComponent : AbpViewComponent
{
    protected IPageLayout PageLayout { get; }

    public ContentTitleViewComponent(IPageLayout pageLayout)
    {
        PageLayout = pageLayout;
    }

    public virtual IViewComponentResult Invoke()
    {
        return View(
             "~/Themes/Mudblazor/Components/ContentTitle/Default.cshtml",
             PageLayout.Content);
    }
}
